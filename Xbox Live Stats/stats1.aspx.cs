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

    [DataContract]
    public class ApiData2
    {
        [DataMember(Name = "contentTitle")]
        public string game_name { get; set; }

        [DataMember(Name = "bingId")]
        public string bingId { get; set; }
    }

    public partial class stats1 : System.Web.UI.Page
    {
        public const string api_key = "f59c5c021ae10521d76a09d2351a58ae16f6f582";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            string gamertag = TextBox1.Text;
            string xuid;
            string jsonTemp;

            HttpClient client = new HttpClient();

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

                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonTemp)))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ApiData));
                        ApiData obj = (ApiData)serializer.ReadObject(stream);

                        Label4.Visible = true;
                        Label4.Text = "Gamerscore: " + obj.GS;
                        Label5.Visible = true;
                        Label5.Text = "Account tier: " + obj.Tier;
                        Label6.Visible = true;
                        Label6.Text = "Reputation: " + obj.rep;
                        Label7.Visible = true;
                        Image1.Visible = true;
                        Image1.ImageUrl = obj.Picture;

                        getImage(obj, xuid, obj2);
                    }
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

        public void getImage(ApiData obj1, string x, ApiData2 obj2)
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

            gfx.DrawImage(System.Drawing.Image.FromFile("game_pics/" + x + ".png"),80,20);
            //gfx.DrawString();
            myBitmap.Save("temp.png", ImageFormat.Png);
        }
    }
}