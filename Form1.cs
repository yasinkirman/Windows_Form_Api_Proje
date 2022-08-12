using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using IdentityModel.Client;
using System.Net.Http.Headers;

namespace OrnekData3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        private RESTClient restClient;

        public void Form1_Load(object sender, EventArgs e)
        {
            restClient = new RESTClient("api/AccessToken");
            restClient.SetAuthHeader("Token Keyinizi Girin");
            Buton();
        }

        DataTable dt = new DataTable();
        DataTable dtParameterList = new DataTable();

        public class RESTClient
        {
            private readonly HttpClient client = new HttpClient();

            public RESTClient(string baseAddress)
            {
                client.BaseAddress = new Uri(baseAddress);
            }
            
            public void SetAuthHeader(string parameter)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
            }
            
            public async Task<string> MakeRequestAsync(HttpMethod method, string path, string postContent = "")
            {
                using (HttpContent content = new StringContent(postContent, Encoding.UTF8, "application/json"))
                using (HttpRequestMessage request = new HttpRequestMessage(method, path))
                {
                    using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }
        }

        public async void Buton()
        {
            string getJsonResult =await restClient.MakeRequestAsync(HttpMethod.Get, "api/Employee");     
            Root users = JsonConvert.DeserializeObject<Root>(getJsonResult);
            if (users == null | users.data == null)
            {
                users = null;
            }
            else
            {
                
            }
            dt.Clear();
            dt.Columns.Add("Id");
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Surname");
            dt.Columns.Add("Birthday");
            dt.Columns.Add("Gender");
            var apidata = users;
            if (apidata != null)
            {
                foreach (var ndata in apidata.data)
                {
                    dt.Rows.Add(ndata.id, ndata.code ,ndata.name, ndata.surname, ndata.birthday, ndata.gender); 
                }
                this.dataGridView1.DataSource = dt;
                DataGridViewBand band = dataGridView1.Columns[0];
                band.Visible = false;
                DataGridViewBand bandBirthDay = dataGridView1.Columns[4];
                bandBirthDay.Visible = false;
                DataGridViewBand bandGender = dataGridView1.Columns[5];
                bandGender.Visible = false;
            }

        }
        public async void Parameter()
        {
            string getJsonParameter = await restClient.MakeRequestAsync(HttpMethod.Get, "api/ParameterList");
            Root usersParameter = JsonConvert.DeserializeObject<Root>(getJsonParameter);
            if (usersParameter == null | usersParameter.data == null)
            {
                usersParameter = null;
            }
            else
            {
                
            }
            dtParameterList.Columns.Add("ParameterId");
            dtParameterList.Columns.Add("displayName");
            var apidataParameter = usersParameter;
            if (apidataParameter != null)
            {
                foreach (var ndataParameter in apidataParameter.data)
                {
                    dtParameterList.Rows.Add(ndataParameter.id, ndataParameter.displayName);
                }
                this.dataGridView2.DataSource = dtParameterList;    
            }
        }           
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("code+name+' '+surname like '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            Form2 form2 = new Form2();
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                form2.label2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                form2.label4.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                form2.label6.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                form2.label8.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                form2.label10.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                form2.label12.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                form2.Show();
            }           
        }   

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void simgeDurumunaKüçültToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
