using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Data.Sql;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Xbox_Live_Stats.Properties;

namespace Xbox_Live_Stats
{
    [DataContract]
    public class ApiData
    {
        [DataMember(Name = "Gamerscore")]
        public string GS { get; set; }

        [DataMember(Name = "GameDisplayPicRaw")]
        public string Picture { get; set; }

        [DataMember(Name = "AccountTier")]
        public string Tier { get; set; }

        [DataMember(Name = "XboxOneRep")]
        public string rep { get; set; }
    }

    public class Activity
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int numShares { get; set; }
        public int numLikes { get; set; }
        public int numComments { get; set; }
        public object ugcCaption { get; set; }
        public string activityItemType { get; set; }
        public object userXuid { get; set; }
        public string date { get; set; }
        public string contentType { get; set; }
        public int titleId { get; set; }
        public string platform { get; set; }
        public string sandboxid { get; set; }
        public object userKey { get; set; }
    }
    public class AuthorInfo
    {
        public string name { get; set; }
        public string secondName { get; set; }
        public string imageUrl { get; set; }
        public string authorType { get; set; }
        public object id { get; set; }
    }
    public class RootObject
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int sessionDurationInMinutes { get; set; }
        public string contentImageUri { get; set; }
        public string bingId { get; set; }
        public string contentTitle { get; set; }
        public string vuiDisplayName { get; set; }
        public string platform { get; set; }
        public int titleId { get; set; }
        public Activity activity { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public bool hasUgc { get; set; }
        public string activityItemType { get; set; }
        public string contentType { get; set; }
        public string shortDescription { get; set; }
        public string itemText { get; set; }
        public string itemImage { get; set; }
        public string shareRoot { get; set; }
        public string feedItemId { get; set; }
        public string itemRoot { get; set; }
        public bool hasLiked { get; set; }
        public AuthorInfo authorInfo { get; set; }
        public string gamertag { get; set; }
        public string realName { get; set; }
        public string displayName { get; set; }
        public string userImageUri { get; set; }
        public object userXuid { get; set; }
    }

    public partial class stats1 : System.Web.UI.Page
    {
        public const string api_key = "f59c5c021ae10521d76a09d2351a58ae16f6f582";
        public const string connection_string = "Server=tcp:a5fb6dp2o2.database.windows.net,1433;Database=Project 1;User ID=quantum2@a5fb6dp2o2;Password=aaabbc123+;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            string gamertag = TextBox1.Text;
            string xuid;
            string jsonTemp;

            HttpClient client = new HttpClient();
            SqlConnection myConnection = new SqlConnection(connection_string);
            myConnection.Open();

            client.BaseAddress = new Uri("https://xboxapi.com");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-AUTH", api_key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Label2.Text = "Searching...";
                var resp1 = await client.GetAsync("/v2/xuid/" + gamertag);

                xuid = await resp1.Content.ReadAsStringAsync();
                if (xuid.Length == 16)
                {
                    Label2.Text = "Gamertag found !";
                    Label3.Visible = true;
                    Label3.Text = "XUID: " + xuid;

                    resp1 = await client.GetAsync("/v2/" + xuid + "/profile");
                    jsonTemp = await resp1.Content.ReadAsStringAsync();

                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonTemp));
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ApiData));
                    ApiData obj = (ApiData)serializer.ReadObject(stream);

                    //Inserir dados na base de dados
                    SqlCommand myCommand = new SqlCommand("INSERT INTO Gamertags(XUID, Gamertag) " + "Values ('" + xuid + "', '" + gamertag +"')", myConnection);
                    myCommand.ExecuteNonQuery();

                    resp1 = await client.GetAsync("/v2/" + xuid + "/activity/recent");
                    var jsonTemp2 = await resp1.Content.ReadAsStringAsync();

                    var results = JsonConvert.DeserializeObject<List<RootObject>>(jsonTemp2);

                    Label4.Visible = true;
                    Label4.Text = "Gamerscore: " + obj.GS;
                    Label5.Visible = true;
                    Label5.Text = "Account tier: " + obj.Tier;
                    Label6.Visible = true;
                    Label6.Text = "Reputation: " + obj.rep;
                    Label7.Visible = true;
                    Image1.Visible = true;
                    Image1.ImageUrl = obj.Picture;

                    getImage(obj, xuid, gamertag, results);
                }
                else
                {
                    Label2.Text = "Error ! Check if the gamertag is correct";
                }
            }
            catch(Exception e1)
            {
                throw e1;
            }
        }

        public void getImage(ApiData obj1, string x, string gamertag, List<RootObject> obj2)
        {
            System.Drawing.Image back1 = Resources.test2;
            Bitmap myBitmap = new Bitmap(back1);
            Graphics gfx = Graphics.FromImage(myBitmap);

            using (WebClient Client = new WebClient())
            {
                if (!Directory.Exists("game_pics"))
                {
                    DirectoryInfo di = Directory.CreateDirectory("game_pics");
                }

                Client.DownloadFile(obj1.Picture, "game_pics/" + x + ".png");
            }

            gfx.DrawImage(System.Drawing.Image.FromFile("game_pics/" + x + ".png"), 80, 20);

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(150.0F, 150.0F);

            gfx.DrawString(gamertag, drawFont, drawBrush, drawPoint);

            myBitmap.Save("temp.png", ImageFormat.Png);
        }
    }
}