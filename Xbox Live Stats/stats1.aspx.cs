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
            System.Drawing.Image back1;

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

                        back1 = Resources.test1;
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
    }
}