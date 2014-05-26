
//
// Name: Form1.cs
// Function: Interface of  A Patent Document Building System
// Author: Zhuang, Chao
// Date: 2010/10/20
// Copyright 2010   Version0.7
// Cogexplorer Design Studio, 
//
// Copyright 2012-3-18
// Open Source code, Using GPLv2.0 license.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Xml.Schema;
using System.Xml.Linq;

namespace PatentMind
    {
       public partial class Form1 : Form
        {
        public int methodcount = 0;
        public int devicecount = 0;
        public int rightreqcount = 0;
        public int rightmethodcount = 0;
        public int rightdevicecount = 0;
        public int rightfeaturecount = 0;
        public int printOption = 0;
        public int CurrentPatentPrj = 0;
        public string InventionDateDIR;

        public string PatentfileName1 = "PatentDoc.txt";
        public string RightfileName1 = "RightDoc.txt";

        public string InstallDirString = Application.StartupPath;
        public string CurrentDirString = Application.StartupPath + "\\CurrentDir";
        public string UnfinishedDirString = Application.StartupPath + "\\UnfinishedDir"; 
        public int EnterBool =0;

        public Form1()
            {
            // Image and Data Directory already established

       
            InitializeComponent();

            }

        private void button1_Click(object sender, EventArgs e)
            {
            // Button: 初始化设置
            // Initialization Date and Directory, Set 2 XML
            // InstallDirString + \\Invention 在安装的时候就已经建立。
            
            Directory.CreateDirectory(CurrentDirString);
            Directory.CreateDirectory(UnfinishedDirString);

            MessageBox.Show("注意: 由于可能有多个专利，专利项目是以初始技术交底书的设置时间作为该项目子目录");

            tabControl1.SelectedTab = tabPage3;
            textBox2.Text = "";
            
            dateTimePicker1.CustomFormat = "dd MMMM, yyyy-dddd";
            string InventionDate = dateTimePicker1.Value.ToLongDateString();
            InventionDateDIR = InstallDirString + "\\Invention\\Invention" + dateTimePicker1.Value.ToLongDateString();
     
            // Get Information from CurrentPatentBasic.Xml   (beginning Jiaodi status and Jiaodi Date,Write them into xml)
            
            XDocument UnfinishedXML = XDocument.Load(InstallDirString+"\\Unfinished.xml");
            XElement     UnfinishedRoot = UnfinishedXML.Element("Root");
            XAttribute   UnfinishedAttribute = UnfinishedRoot.Attribute("UnfinishedState");

            //int UnfinishedState=0;
            int UnfinishedState= Convert.ToInt32(UnfinishedAttribute.Value);
            int JiaodiState = 1;

            if  (Equals(UnfinishedState, 0))
                { 
                //  Set First CurrentPatentBasic.xml
                   UnfinishedState = UnfinishedState + 1; 
                   XDocument CurrentPatentXML = XDocument.Load("CurrentPatentBasic.xml");
                   XElement PBasicRoot = CurrentPatentXML.Element("Root");
                   XElement PBasicItem = new XElement("PBasicItem",
                                                                                    new XAttribute("UnfinishedState", UnfinishedState ),
                                                                                    new XElement("JiaodiDate", InventionDate),
                                                                                    new XElement("JiaodiState", JiaodiState.ToString())
                                                                    
                                                                                    );
                   PBasicRoot.Add(PBasicItem);
                   CurrentPatentXML.Save("CurrentPatentBasic.xml");
          
                }

            UnfinishedRoot.SetAttributeValue("UnfinishedState", UnfinishedState.ToString());
            UnfinishedXML.Save("Unfinished.xml");

            }

        private void button4_Click(object sender, EventArgs e)
            {
            // Button: 内涵智能检查, 计算检查
            richTextBox1.AppendText("权利要求");
            rightreqcount = rightreqcount + 1;
            if (rightreqcount == 1)
                {
                textBox2.AppendText("第二步：权利要求描述项对比计数......\n");
                }
            richTextBox1.AppendText(rightreqcount.ToString());
            label11.Text = rightreqcount.ToString();
            richTextBox1.AppendText("\n");
            }

        private void Form1_Load(object sender, EventArgs e)
            {
            textBox2.AppendText("第一步进入认知图导引器\n");
            textBox2.AppendText("\n");
            textBox2.AppendText("下面各个步骤, 可以按照认知图导引步骤“点击-跳转”，逐步完成专利文档构造。\n");
            textBox2.AppendText("\n");

            textBox2.AppendText("参照专利文本样本，可以点击右上角的“专利文本解释”按钮，查看专利文本各项说明。\n");
            textBox2.AppendText("\n");
            textBox2.AppendText("专利通过最后的文档聚合格式转换完成（点击“文档格式转换”按钮进入聚合操作）。\n");

            }

        private void button5_Click(object sender, EventArgs e)
            {
            //Button: 方法权利计数
            richTextBox1.AppendText("本方法包括.A B C.步骤, 步骤1:           步骤2:              步骤3:");
            rightmethodcount = rightmethodcount + 1;
            label12.Text = rightmethodcount.ToString();
            richTextBox1.AppendText("  \n");
            richTextBox1.AppendText("  \n");
            }

        private void button3_Click(object sender, EventArgs e)
            {
            //Button: 产品技术特征
            textBox2.AppendText("产品技术组块");
            devicecount = devicecount + 1;
            textBox2.AppendText(devicecount.ToString());
            label8.Text = devicecount.ToString();
            textBox2.AppendText("   \n");

            }

        private void button2_Click(object sender, EventArgs e)
            {
            //Button: 方法技术特征
            textBox2.AppendText("方法技术组块");
            methodcount = methodcount + 1;
            textBox2.AppendText(methodcount.ToString());
            label7.Text = methodcount.ToString();
            textBox2.AppendText("   \n");

            }

        private void button7_Click(object sender, EventArgs e)
            {
            // Button: 产品权利计数
            richTextBox1.AppendText("本产品包括.A B C .部分, 主要包括A:    B:    C:");
            rightdevicecount = rightdevicecount + 1;
            label13.Text = rightdevicecount.ToString();
            richTextBox1.AppendText("  \n");
            }

        private void button10_Click(object sender, EventArgs e)
            {
            //  Button: 退出
            // Get Unfinished and finished information
            // Finished =1  , Exit
            XDocument FinishedXml1= XDocument.Load(InstallDirString+"\\finished.xml");
            XElement FinishedRoot = FinishedXml1.Element("Root");
            int FinishedState1 = Convert.ToInt32(FinishedRoot.Attribute("FinishedState").Value);
            /*
            if (Equals(FinishedState1, 0))    // Unfinished -> MOVE
                {
                      //Directory.Move(CurrentDirString, UnfinishedDirString );
                     
                      File.Move(CurrentDirString +"\\CurrentPatentBasic.xml", UnfinishedDirString +"\\CurrentPatentBasic.xml");
                      File.Move(CurrentDirString+"\\TechFeature.xml", UnfinishedDirString+"\\TechFeature.xml");
                      File.Move(CurrentDirString+"\\RightSpec.xml",    UnfinishedDirString +"\\RightSpec.xml");
                      File.Move(CurrentDirString+"\\MethodSpec.xml", UnfinishedDirString +"\\MethodSpec.xml");
                      File.Move(CurrentDirString+"\\DeviceSpec.xml", UnfinishedDirString   +"\\DeviceSpec.xml");            
                    
                }
                else    // Current-> MOVE inventionDateDIR
                    {
                    //  Directory.Move(CurrentDirString, InventionDateDIR);
                      
                       File.Move(CurrentDirString+"\\CurrentPatentBasic.xml", InventionDateDIR+"\\CurrentPatentBasic.xml");
                       File.Move(CurrentDirString+"\\TechFeature.xml", InventionDateDIR+"\\TechFeature.xml");
                       File.Move(CurrentDirString+"\\RightSpec.xml", InventionDateDIR+"\\RightSpec.xml");
                       File.Move(CurrentDirString+"\\MethodSpec.xml", InventionDateDIR+"\\MethodSpec.xml");
                       File.Move(CurrentDirString+"\\DeviceSpec.xml", InventionDateDIR+"\\DeviceSpec.xml");
                                          
                     }
              */ 
              Application.Exit();
            }

        private void button9_Click(object sender, EventArgs e)
            {
            //Button: 生成权利文本
            //need check 2 count,then yes,else messagebox
            label15.Text = "Yes";
            textBox2.AppendText("  \n");
            textBox2.AppendText("第四步：通过多级聚集文档，生成专利申请文档");
            textBox2.AppendText("  \n");
            //save text
            }

        private void button8_Click(object sender, EventArgs e)
            {
            //Button: 生成权利要求:   通过一级聚集文档，生成权利要求书的最终结果。

            //nne check 2 count, then yes, else messagebox
            label14.Text = "Yes";
            textBox2.AppendText("  \n");
            textBox2.AppendText("第三步：通过一级聚集文档，生成生成权利要求书的最终结果");
            textBox2.AppendText("  \n");
            //save text
            }

        private void button12_Click(object sender, EventArgs e)
            {
            //在文档缓冲中显示提示语
            textBox20.Text = textBox12.Text;      //PatentName
            textBox3.Text = textBox12.Text;
            textBox14.Text = "";
            //通过多级聚集文档，生成专利申请文档
            string MidString;
            textBox14.AppendText("  \n");
            textBox14.AppendText(textBox12.Text);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            textBox14.AppendText("技术领域");
            textBox14.AppendText("  \n");
            textBox14.AppendText(textBox6.Text);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            textBox14.AppendText("背景技术");
            textBox14.AppendText("  \n");
            MidString = textBox7.Text;
            textBox14.AppendText(MidString);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            textBox14.AppendText("发明内容");
            textBox14.AppendText("  \n");
            MidString = textBox8.Text;
            textBox14.AppendText(MidString);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            textBox14.AppendText("附图说明");
            textBox14.AppendText("  \n");
            MidString = textBox9.Text;
            textBox14.AppendText(MidString);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            textBox14.AppendText("具体实施方式");
            textBox14.AppendText("  \n");
            MidString = textBox10.Text;
            textBox14.AppendText(textBox10.Text);
            textBox14.AppendText("  \n");
            textBox14.AppendText("  \n");
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            //this.webBrowser1.Url = new System.Uri("http://www.cogexplorer.com/PatentLaw/PatentLawIndex.html", System.UriKind.Absolute);
            // this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            }

        private void button11_Click(object sender, EventArgs e)
            {
            richTextBox2.Text = richTextBox1.Text;
            label14.Text = "Yes";
            }

        private void button15_Click(object sender, EventArgs e)
            {
            //save the final patent file
            string FileName;
            // Further file name can be changed in textbox
            FileName = "PatentDoc.txt";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files(*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                FileName = saveFileDialog.FileName;
                MessageBox.Show(FileName);
                }
            //FileStream fout = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(FileName);
            MessageBox.Show(textBox14.Text);

            // write file according to line; add file format information "\n";
            // this need system approach to design this file format (change line)
            String str14 = textBox14.Text;
            sw.WriteLine(str14);
            sw.Close();

            }



        private void button16_Click(object sender, EventArgs e)
            {
            MessageBox.Show("技术交底书文件格式为txt格式, 如果有附图和照片在附加图管理器处理");
            string textLine;
            textBox1.Text = "";

            OpenFileDialog fileChooser = new OpenFileDialog();
            DialogResult result = fileChooser.ShowDialog();
            String fileName = fileChooser.FileName;
            if (result == DialogResult.Cancel)
                return;
            if (fileName == " " || fileName == null)
                MessageBox.Show("No File Exist");
            else
                {
                FileStream fileInput = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader StreamInput = new StreamReader(fileInput);
                while ((textLine = StreamInput.ReadLine()) != null)
                    {
                    // MessageBox.Show(textLine);
                    textBox1.AppendText(textLine);
                    textBox1.AppendText(" ");
                    textBox1.AppendText("\n");
                    }
                fileInput.Close();
                }
            }

        private void button17_Click(object sender, EventArgs e)
            {
            // Using CurrentPatentBasic.xml 

            MessageBox.Show("开始申请");

            XDocument CurrentPatent = XDocument.Load("CurrentPatentBasic.xml");
            XElement CurrentPatentRoot = CurrentPatent.Element("Root");

            //  using TextBox to fill the form pattern PatentNo + PatentName  
            //  Set Name

            CurrentPatentRoot.Add(new XElement("PatentName", textBox3.Text));
            CurrentPatentRoot.Add(new XElement("PatentAppNo", textBox21.Text));
            CurrentPatentRoot.Add(new XElement("PatentNo", textBox22.Text));

            CurrentPatent.Save("CurrentPatentBasic.xml");

            // Change Button Color
            button17.BackColor = textBox2.BackColor;
            }

        private void button14_Click(object sender, EventArgs e)
            {
            //Button: 初始化帮助
            // Init +  Sample Blind Man Learning Data 
            MessageBox.Show("This is the HELP of  initialization");

            }


        private void button27_Click(object sender, EventArgs e)
            {
            //  Using Mail Attachment to Send the Evidence Mail Attachment.
            MessageBox.Show("检查是否已经完成电子邮件通信设置");
            string SmtpServerName = "Smtp.126.com";
            try
                {
                MailMessage Mail = new MailMessage(textBox15.Text, textBox16.Text);
                //  Mail.SmtpServer = "Smtp.gmail.com";
                Mail.Subject = textBox13.Text.Trim().ToString();
                Mail.Body = textBox11.Text.Trim().ToString();                 // Ad3d Mail Filter for M.Glue Script

                //  Attachment PatentDoc = new Attachment("PatentDoc.rtf");
                //  PatentfileName1 == PatentDoc.rtf   (RightDoc.rtf)

                Attachment PatentDoc = new Attachment(linkLabel2.Text);
                Mail.Attachments.Add(PatentDoc);

                Attachment RightDoc = new Attachment(linkLabel1.Text);
                Mail.Attachments.Add(RightDoc);
                //  Attachment3

                Attachment ShuoMingDoc = new Attachment(linkLabel3.Text);
                Mail.Attachments.Add(ShuoMingDoc);

                //  Attachment4
                Attachment ShuoMingDiagram = new Attachment(linkLabel4.Text);
                Mail.Attachments.Add(ShuoMingDiagram);

                //  Attachment5
                Attachment ShuoMingAbstract = new Attachment(linkLabel5.Text);
                Mail.Attachments.Add(ShuoMingAbstract);

                Attachment AbstractDiagram = new Attachment(linkLabel6.Text);
                Mail.Attachments.Add(AbstractDiagram);
     

                /*
                SmtpClient Client = new SmtpClient("smtp.126.com", 25);
                Client.Credentials = new System.Net.NetworkCredential("coursetest@126.com", "123456"); 
                */
                switch (comboBox1.SelectedIndex)
                    {
                    case 0: SmtpServerName = "smtp.gmail.com"; break;
                    case 1: SmtpServerName = "smtp.126.com"; break;
                    case 2: SmtpServerName = "smtp.163.com"; break;
                    case 3: SmtpServerName = "smtp.qq.com"; break;
                    case 4: SmtpServerName = "smtp.sina.com"; break;
                    case 5: SmtpServerName = "smtp.sohu.com"; break;
                    case 6: SmtpServerName = "smtp.yahoo.cn"; break;
                    case 7: SmtpServerName = "smtp.hotmail.com"; break;
                    }
     
                SmtpClient Client = new SmtpClient(SmtpServerName, 25);
                Client.Credentials = new System.Net.NetworkCredential(textBox18.Text.Trim().ToString(),
                                                                      textBox17.Text.Trim().ToString());
                Client.Send(Mail);
                MessageBox.Show("发送成功");
                }
            catch (Exception errormessage)
                {
                MessageBox.Show(errormessage.Message);
                }

            }

        private void button13_Click(object sender, EventArgs e)
            {
            // Using LinQ to Query the status of  Application CurrentPatentBasic.xml
            // Return a MessageBOX
            MessageBox.Show("专利状态查询");

            }

        private void button25_Click(object sender, EventArgs e)
            {
            //  Using it for Comparising Intensional Engine for two Document
            //  Corresponding Line,Row,Column 

            string textLine;
            OpenFileDialog fileChooser = new OpenFileDialog();
            DialogResult result = fileChooser.ShowDialog();
            String fileName = fileChooser.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (fileName == " " || fileName == null)
                MessageBox.Show("No File Exist");
            else
                {
                FileStream fileInput = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader StreamInput = new StreamReader(fileInput);
                while ((textLine = StreamInput.ReadLine()) != null)
                    {
                    // MessageBox.Show(textLine);
                    richTextBox3.AppendText(textLine);
                    richTextBox3.AppendText(" ");
                    richTextBox3.AppendText("\n");
                    richTextBox4.AppendText(textLine);
                    richTextBox4.AppendText(" ");
                    richTextBox4.AppendText("\n");
                    }
                fileInput.Close();
                }

            int indexcnt = 0;
            indexcnt = richTextBox3.Text.IndexOf(textBox24.Text);
            //     MessageBox.Show(indexcnt.ToString());
            int indexcnt1 = 0;
            indexcnt1 = richTextBox4.Text.IndexOf(textBox24.Text);
            //    MessageBox.Show(indexcnt1.ToString());

            richTextBox3.Select(indexcnt, textBox24.Text.Length);
            richTextBox4.Select(indexcnt1, textBox24.Text.Length);
            richTextBox3.SelectionBackColor = Color.Blue;
            richTextBox4.SelectionBackColor = Color.Blue;

            }

        private void button32_Click(object sender, EventArgs e)
            {
            //  查看附图的文件列表,并将文件传给tabcontrol的Imagelist;
            openFileDialog1.InitialDirectory = InstallDirString;
            listBox2.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                textBox4.Text = openFileDialog1.InitialDirectory;
                MessageBox.Show(textBox4.Text);
                string[] dir = Directory.GetFileSystemEntries(textBox4.Text.Trim());
                for (int i = 0; i < dir.Length; i++)
                    {
                    listBox2.Items.Add(dir[i].ToString());
                    }

                }
            //show top three image in the bottom line
            }


        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
            {
            // ImageList Imaglist1 = new ImageList(); 
            string ImgPath = listBox2.SelectedItem.ToString();
            FileInfo FImgInfo = new FileInfo(ImgPath);
            string testImgExt = FImgInfo.Extension;
            if (Equals(testImgExt, ".JPG") || Equals(testImgExt, ".jpg"))
                {
                MessageBox.Show("enter this area");
                pictureBox3.Image = Image.FromFile(ImgPath);
                //   Imaglist1.Images.Add(Image.FromFile(ImgPath));

                }
            else
                {
                MessageBox.Show(" This is not a picture");

                }

            }

        private void button33_Click(object sender, EventArgs e)
            {
            // Button: 认知图导引跳转
            tabControl1.SelectedTab = tabPage1;
            // textBox6.Focus();
            textBox2.Text = "";
            textBox2.AppendText("进入认知图导引器......");
            textBox2.AppendText("\n");
            }

        private void button34_Click(object sender, EventArgs e)
            {
            //Button: 工作记录跳转
            tabControl1.SelectedTab = tabPage3;
            textBox2.Text = "";
            textBox2.AppendText("进入工作记录器,完成技术交底书......");
            textBox2.AppendText("\n");

            }

        private void button35_Click(object sender, EventArgs e)
            {
            //Button: 生成权利要求跳转
            tabControl1.SelectedTab = tabPage5;
            textBox2.Text = "";
            textBox2.AppendText("进入权利描述器,生成权利要求书......");
            textBox2.AppendText("\n");
            }

        private void button45_Click(object sender, EventArgs e)
            {
            //Button: 生成专利说明跳转
            tabControl1.SelectedTab = tabPage6;
            textBox2.Text = "";
            textBox2.AppendText("进入文本构造器,生成专利说明书......\n\n");
            textBox2.AppendText("点击右上角<专利文本解释>, 参考专利文本解释.......\n");
            textBox2.AppendText("\n");
            }

        private void button46_Click(object sender, EventArgs e)
            {
            //Button: 进入内涵智能检查器跳转
            tabControl1.SelectedTab = tabPage2;
            textBox2.Text = "";
            textBox2.AppendText("进入内涵智能检查器, 检查专利说明书......\n\n");
            textBox2.AppendText("\n");
            }

        private void button38_Click(object sender, EventArgs e)
            {
            //Button: 另一个入口进入工作记录
            tabControl1.SelectedTab = tabPage3;
            textBox2.Text = "";
            textBox2.AppendText("进入工作记录器,完成技术交底书......");
            textBox2.AppendText("\n");
            }

        private void button37_Click(object sender, EventArgs e)
            {
            //Button: 另一个入口进入权利要求生成器
            tabControl1.SelectedTab = tabPage5;
            textBox2.Text = "";
            textBox2.AppendText("进入权利描述器,生成权利要求书......");
            textBox2.AppendText("\n");
            }

        private void button36_Click(object sender, EventArgs e)
            {
            //Button: 另一个生成文本构成器入口
            tabControl1.SelectedTab = tabPage6;
            textBox2.Text = "";
            textBox2.AppendText("进入文本构造器,生成专利说明书......\n\n");
            textBox2.AppendText("点击右上角<专利文本解释>, 参考专利文本解释.......\n");
            textBox2.AppendText("\n");
            }

        private void button56_Click(object sender, EventArgs e)
            {
            //Button: 进入认知图
            tabControl1.SelectedTab = tabPage1;
            tabControl7.SelectedTab = tabPage30;
            // textBox6.Focus();
            textBox2.Text = "";
            textBox2.AppendText("进入权利认知图......");
            textBox2.AppendText("\n");
            }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
            {
            // 图片选项目

            // ImageList Imaglist1 = new ImageList(); 
            string ImgPath = listBox2.SelectedItem.ToString();
            FileInfo FImgInfo = new FileInfo(ImgPath);
            string testImgExt = FImgInfo.Extension;
            if (Equals(testImgExt, ".JPG") || Equals(testImgExt, ".jpg") || Equals(testImgExt, ".BMP") || Equals(testImgExt, ".bmp"))
                {
                MessageBox.Show("enter this area");
                pictureBox3.Image = Image.FromFile(ImgPath);
                //   Imaglist1.Images.Add(Image.FromFile(ImgPath));

                }
            else
                {
                MessageBox.Show(" This is not a picture");

                }

            }

        private void button41_Click(object sender, EventArgs e)
            {
            tabControl1.SelectedTab = tabPage5;
            textBox2.Text = "";
            textBox2.AppendText("进入权利描述器,处理独立权利......");
            textBox2.AppendText("\n");
            }

        private void button40_Click(object sender, EventArgs e)
            {
            tabControl1.SelectedTab = tabPage5;
            textBox2.Text = "";
            textBox2.AppendText("进入权利描述器,处理从属权利......");
            textBox2.AppendText("\n");
            }

        private void button39_Click(object sender, EventArgs e)
            {
            tabControl1.SelectedTab = tabPage2;
            tabControl4.SelectedTab = tabPage19;
            textBox2.Text = "";
            textBox2.AppendText("进入内涵智能检查器, 检查专利权利计数......\n\n");
            textBox2.AppendText("\n");
            }

        private void button51_Click(object sender, EventArgs e)
            {
            textBox2.Text = "";
            textBox2.AppendText("点击右上角, 先统计技术特征计数......\n");
            textBox2.AppendText("包括产品特征计数和方法特征计数......\n");
            textBox2.AppendText("\n");
            }

        private void button26_Click(object sender, EventArgs e)
            {
            //         Button : 检查权利计数
            textBox31.Text = methodcount.ToString();
            textBox32.Text = devicecount.ToString();
            textBox33.Text = rightmethodcount.ToString();
            textBox34.Text = rightdevicecount.ToString();

            // 首先检查等式是否成立? 如果成立检查为是, 否则提示.
            label11.Text = "Yes";
            }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser1 = new OpenFileDialog();
            DialogResult result = fileChooser1.ShowDialog();
            PatentfileName1 = fileChooser1.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (PatentfileName1 == " " || PatentfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel2.Text = PatentfileName1;
            }

        private void button30_Click(object sender, EventArgs e)
            {
            // all patentDoc in this document
            richTextBox7.Text = textBox14.Text;
            label15.Text = "Yes";
            // Define richTextBox7.Text f
            printOption = 1;
            StreamWriter sw = new StreamWriter("PatentDoc.txt");
            MessageBox.Show(textBox14.Text);

            // write file according to line; add file format information "\n";
            // this need system approach to design this file format (change line)
            String Patentstr = textBox14.Text;
            sw.WriteLine(Patentstr);
            sw.Close();

            }


        private void button44_Click(object sender, EventArgs e)
            {
            // PRINT 

       
            }

        private void button43_Click(object sender, EventArgs e)
            {

        
            }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
            e.Graphics.FillRectangle(Brushes.Red, new Rectangle(150, 500, 500, 500));
            e.Graphics.DrawString("get the text line", new Font("Simsun", 12), Brushes.Black, 600, 150);

         //   printDocument1.Print();

            }
        private void button31_Click(object sender, EventArgs e)
            {

            // Right Document
            richTextBox7.Text = richTextBox2.Text;
            label15.Text = "Yes";
            // Define richTextBox7.Text 
            printOption = 2;
            StreamWriter sw = new StreamWriter("RightDoc.txt");
            MessageBox.Show(textBox14.Text);

            // write file according to line; add file format information "\n";
            // this need system approach to design this file format (change line)
            String Rightstr = richTextBox2.Text;
            sw.WriteLine(Rightstr);
            sw.Close();
            }

        private void button66_Click(object sender, EventArgs e)
            {
            if (rightreqcount > 0)
                {
                DataSet InitDataS = new DataSet();
                //InitDataS.ReadXml(InstallDirString + "\\XML" + "\\Sketch1.XML");
                InitDataS.ReadXml(InstallDirString + "\\TechFeature.Xml");
                dataGridView1.DataSource = InitDataS.Tables[0];
                }
            else
                {
                MessageBox.Show("没有新的技术特征数据");
                }
            }

        private void button69_Click(object sender, EventArgs e)
            {
            // Display the Image List for invention work record
            listBox1.Items.Clear();
            textBox47.Text = InstallDirString + "\\Image";
            // First Check Current File Directory
            if (!Equals(textBox47.Text, InstallDirString + "\\Image"))
                {
                MessageBox.Show("当前的图象工作目录应该在\\InstallPath\\Image上 ");
                label9.Text = InstallDirString + "\\Image";
                }
            else
                {
                OpenFileDialog OpenImageFileDialog = new OpenFileDialog();
                OpenImageFileDialog.InitialDirectory = InstallDirString + "\\Image";

                if (OpenImageFileDialog.ShowDialog() == DialogResult.OK)
                    {
                    textBox47.Text = OpenImageFileDialog.InitialDirectory;
                    string[] ImageDir = Directory.GetFileSystemEntries(textBox47.Text.Trim());
                    for (int i = 0; i < ImageDir.Length; i++)
                        {
                        listBox1.Items.Add(ImageDir[i].ToString());
                        }
                    }
                }
            }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
            string ImgPath = listBox1.SelectedItem.ToString();
            FileInfo FimgInfo = new FileInfo(ImgPath);
            string testImgExt = FimgInfo.Extension;

            if (Equals(testImgExt, ".JPG") || Equals(testImgExt, ".jpg") || Equals(testImgExt, ".BMP") || Equals(testImgExt, ".bmp"))
                {
                MessageBox.Show("转到[查看图象]项查看当前图象");
                pictureBox7.Image = Image.FromFile(ImgPath);
                }
            else
                {
                MessageBox.Show("当前文件格式不是JPG或者BMP,不能显示");
                }
            }

        private void button65_Click(object sender, EventArgs e)
            {
            //  Next TechFeature.Xml

            rightreqcount = rightreqcount + 1;
            textBox44.Text = rightreqcount.ToString();

            XDocument PatentTech = XDocument.Load(InstallDirString+"\\TechFeature.xml");
            XElement TPatentTechR = PatentTech.Element("Root");
            XElement TPatentTech = new XElement("TEPatentItem",
                                                                                  new XAttribute("ID", rightreqcount.ToString()));

            /*
             XElement TPatentCurrent = PatentTech.Element("TPatentCurrent");
             XElement TPatentQ = PatentTech.Element("TPatentQ");
             XElement TPatentTechS = PatentTech.Element("TPatentTechS");
             XElement TPatentAdvan = PatentTech.Element("TPatentAdvan");
             */
            TPatentTech.Add(new XElement("技术特征名", textBox48.Text));
            TPatentTech.Add(new XElement("当前技术", textBox40.Text));
            TPatentTech.Add(new XElement("现有问题", textBox41.Text));
            TPatentTech.Add(new XElement("区别特征", textBox42.Text));
            TPatentTech.Add(new XElement("技术优点", textBox43.Text));

            TPatentTechR.Add(TPatentTech);

            PatentTech.Save(InstallDirString +"\\TechFeature.xml");

            textBox40.Text = "";
            textBox41.Text = "";
            textBox42.Text = "";
            textBox43.Text = "";
            textBox48.Text = "";
            }

        private void button70_Click(object sender, EventArgs e)
            {
            textBox44.Text = "";
            textBox45.Text = "";
            textBox46.Text = "";
            }

        private void button67_Click(object sender, EventArgs e)
            {
            methodcount = methodcount + 1;
            textBox45.Text = methodcount.ToString();
            label7.Text = methodcount.ToString();
            }

        private void button68_Click(object sender, EventArgs e)
            {
            devicecount = devicecount + 1;
            textBox46.Text = devicecount.ToString();
            label8.Text = devicecount.ToString();
            }

        private void button29_Click(object sender, EventArgs e)
            {
            //  Intensional Semantics Engine for Comparision
            //  Get Items from TechFeature.xml
            XDocument TechFeaturexml = XDocument.Load("TechFeature.xml");
            XElement TechItemRoot = TechFeaturexml.Element("Root");
            IEnumerable<XElement> TechItems = TechItemRoot.Elements();
            if (Equals(EnterBool, 0))
                {
                foreach (XElement TechItem in TechItems)
                    {
                    XElement TechItemName = TechItem.Element("技术特征名");
                    comboBox5.Items.Add(TechItemName.Value);
                    EnterBool = 1;
                    }
                }

            }
            

        private void button72_Click(object sender, EventArgs e)
            {
            int indexcnt = 0;
            indexcnt = richTextBox3.Text.IndexOf(textBox24.Text);
            MessageBox.Show(indexcnt.ToString());
            int indexcnt1 = 0;
            indexcnt1 = richTextBox4.Text.IndexOf(textBox24.Text);
            MessageBox.Show(indexcnt1.ToString());

            richTextBox3.Select(indexcnt, textBox24.Text.Length);
            richTextBox4.Select(indexcnt1, textBox24.Text.Length);

            richTextBox3.SelectionBackColor = Color.Blue;
            richTextBox4.SelectionBackColor = Color.Blue;
            }

        private void button82_Click(object sender, EventArgs e)
            {
            if (rightfeaturecount > 0)
                {
                DataSet InitDataS = new DataSet();
                //InitDataS.ReadXml(InstallDirString + "\\XML" + "\\Sketch1.XML");
                InitDataS.ReadXml(InstallDirString + "\\RightSpec.Xml");
                dataGridView2.DataSource = InitDataS.Tables[0];
                }
            else
                {
                MessageBox.Show("没有新的技术特征数据");
                }
            }

        private void button77_Click(object sender, EventArgs e)
            {
            // Button: 方法权利
            rightmethodcount = rightmethodcount + 1;
            rightfeaturecount = rightfeaturecount + 1;
            label12.Text = rightmethodcount.ToString();



            XDocument PatentTechMF = XDocument.Load("MethodSpec.xml");
            XElement TPatentTechR = PatentTechMF.Element("Root");
            XElement TPatentTechMF = new XElement("TEPatentItemF",
                                                                                  new XAttribute("ID", rightmethodcount.ToString()));

            TPatentTechMF.Add(new XElement("技术特征项", textBox55.Text));
            TPatentTechMF.Add(new XElement("引用部分", textBox54.Text));
            TPatentTechMF.Add(new XElement("限定部分", textBox53.Text));
            // TPatentTechMF.Add(new XElement("区别特征", textBox42.Text));
            // TPatentTechMF.Add(new XElement("技术优点", textBox43.Text));

            TPatentTechR.Add(TPatentTechMF);

            PatentTechMF.Save("MethodSpec.xml");

            /*
            XDocument PatentTechRight = XDocument.Load("RightSpec.xml");
            XElement PatentTechRightRoot = PatentTechRight.Element("Root");
            XElement PatentTechRightF = new XElement("PatentRightCount",
                                                                                  new XAttribute("ID", rightfeaturecount.ToString()));

            PatentTechRightF.Add(new XElement("技术特征项", textBox55.Text));
            PatentTechRightF.Add(new XElement("前序部分", textBox54.Text));
            PatentTechRightF.Add(new XElement("特征部分", textBox53.Text));
            // TPatentTechMF.Add(new XElement("区别特征", textBox42.Text));
            // TPatentTechMF.Add(new XElement("技术优点", textBox43.Text));

            PatentTechRightRoot.Add(PatentTechRightF);

            PatentTechRight.Save("RightSpec.xml");
            */ 
            textBox55.Text = "";
            textBox54.Text = "";
            textBox53.Text = "";
            //textBox43.Text = "";
            //textBox48.Text = "";
            }

        private void button83_Click(object sender, EventArgs e)
            {
            // Display Method XML
            if (rightmethodcount > 0)
                {
                DataSet InitDataS = new DataSet();
                //InitDataS.ReadXml(InstallDirString + "\\XML" + "\\Sketch1.XML");
                InitDataS.ReadXml(InstallDirString + "\\MethodSpec.Xml");
                dataGridView2.DataSource = InitDataS.Tables[0];
                }
            else
                {
                MessageBox.Show("没有新的方法权利要求数据");
                }
            }

        private void button84_Click(object sender, EventArgs e)
            {   // Display Device XML
            if (rightdevicecount > 0)
                {
                DataSet InitDataS = new DataSet();
                //InitDataS.ReadXml(InstallDirString + "\\XML" + "\\Sketch1.XML");
                InitDataS.ReadXml(InstallDirString + "\\DeviceSpec.Xml");
                dataGridView2.DataSource = InitDataS.Tables[0];
                }
            else
                {
                MessageBox.Show("没有新的产品权利要求数据");
                }

            }

        private void button73_Click(object sender, EventArgs e)
            {
            // Button:  产品权利
            rightdevicecount = rightdevicecount + 1;
            rightfeaturecount = rightfeaturecount + 1;
            label13.Text = rightdevicecount.ToString();

            XDocument PatentTechDF = XDocument.Load("DeviceSpec.xml");
            XElement TPatentTechR = PatentTechDF.Element("Root");
            XElement TPatentTechDF = new XElement("TEPatentItemF",
                                                                                  new XAttribute("ID", rightdevicecount.ToString()));

            TPatentTechDF.Add(new XElement("技术特征项", textBox58.Text));
            TPatentTechDF.Add(new XElement("引用部分", textBox57.Text));
            TPatentTechDF.Add(new XElement("限定部分", textBox56.Text));
            // TPatentTechMF.Add(new XElement("区别特征", textBox42.Text));
            // TPatentTechMF.Add(new XElement("技术优点", textBox43.Text));

            TPatentTechR.Add(TPatentTechDF);

            PatentTechDF.Save("DeviceSpec.xml");

            /*
            XDocument PatentTechRight = XDocument.Load("RightSpec.xml");
            XElement PatentTechRightRoot = PatentTechRight.Element("Root");
            XElement PatentTechRightF = new XElement("PatentRightCount",
                                                                                  new XAttribute("ID", rightfeaturecount.ToString()));

            PatentTechRightF.Add(new XElement("技术特征项", textBox58.Text));
            PatentTechRightF.Add(new XElement("前序部分", textBox57.Text));
            PatentTechRightF.Add(new XElement("特征部分", textBox56.Text));
            // TPatentTechMF.Add(new XElement("区别特征", textBox42.Text));
            // TPatentTechMF.Add(new XElement("技术优点", textBox43.Text));

            PatentTechRightRoot.Add(PatentTechRightF);

            PatentTechRight.Save("RightSpec.xml");
            */

            textBox60.AppendText("根据权利要求");
            textBox60.AppendText(rightfeaturecount.ToString());
            textBox60.AppendText("所述的");
            textBox60.AppendText(textBox58.Text);
            textBox60.AppendText("的产品, 其特征在于,");
            textBox60.AppendText(textBox56.Text);
            textBox60.AppendText("\n");
            textBox60.AppendText("\n");

            textBox58.Text = "";
            textBox57.Text = "";
            textBox56.Text = "";

            }

        private void button85_Click(object sender, EventArgs e)
            {
            //Sample
            textBox12.Text = " 专利名称(发明主题): 反映发明和实用新型要求保护技术的主题和类型(产品和方法) ";
            textBox6.Text = "指发明或者实用新型要求保护的技术方案所属或者直接应用的具体技术领域";

            string textLine;

            string filename1 = InstallDirString + "\\data\\patentdata1.txt";      //以后加上完整路径,路径可以在开始设定
            string filename2 = InstallDirString + "\\data\\patentdata2.txt";
            string filename3 = InstallDirString + "\\data\\patentdata3.txt";
            string filename4 = InstallDirString + "\\data\\patentdata4.txt";

            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

            FileStream fileInput1 = new FileStream(filename1, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput1 = new StreamReader(fileInput1);
            while ((textLine = StreamInput1.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox7.AppendText(textLine);
                textBox7.AppendText(" ");
                textBox7.AppendText("\n");
                }
            fileInput1.Close();

            FileStream fileInput2 = new FileStream(filename2, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput2 = new StreamReader(fileInput2);
            while ((textLine = StreamInput2.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox8.AppendText(textLine);
                textBox8.AppendText(" ");
                textBox8.AppendText("\n");
                }
            fileInput2.Close();


            FileStream fileInput3 = new FileStream(filename3, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput3 = new StreamReader(fileInput3);
            while ((textLine = StreamInput3.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox9.AppendText(textLine);
                textBox9.AppendText(" ");
                textBox9.AppendText("\n");
                }
            fileInput3.Close();


            FileStream fileInput4 = new FileStream(filename4, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput4 = new StreamReader(fileInput4);
            while ((textLine = StreamInput4.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox10.AppendText(textLine);
                textBox10.AppendText(" ");
                textBox10.AppendText("\n");
                }
            fileInput4.Close();

            }

        private void button86_Click(object sender, EventArgs e)
            {
            textBox12.Text = " 发明主题: 盲人声音字典学习系统";
            textBox6.Text = "指发明或者实用新型要求保护的技术方案所属或者直接应用的具体技术领域";

            string textLine;

            string filename1 = InstallDirString + "\\data\\patentdata5.txt";
            string filename2 = InstallDirString + "\\data\\patentdata6.txt";
            string filename3 = InstallDirString + "\\data\\patentdata7.txt";
            string filename4 = InstallDirString + "\\data\\patentdata8.txt";

            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

            FileStream fileInput1 = new FileStream(filename1, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput1 = new StreamReader(fileInput1);
            while ((textLine = StreamInput1.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox7.AppendText(textLine);
                textBox7.AppendText(" ");
                textBox7.AppendText("\n");
                }
            fileInput1.Close();

            FileStream fileInput2 = new FileStream(filename2, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput2 = new StreamReader(fileInput2);
            while ((textLine = StreamInput2.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox8.AppendText(textLine);
                textBox8.AppendText(" ");
                textBox8.AppendText("\n");
                }
            fileInput2.Close();


            FileStream fileInput3 = new FileStream(filename3, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput3 = new StreamReader(fileInput3);
            while ((textLine = StreamInput3.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox9.AppendText(textLine);
                textBox9.AppendText(" ");
                textBox9.AppendText("\n");
                }
            fileInput3.Close();


            FileStream fileInput4 = new FileStream(filename4, FileMode.Open, FileAccess.Read);
            StreamReader StreamInput4 = new StreamReader(fileInput4);
            while ((textLine = StreamInput4.ReadLine()) != null)
                {
                // MessageBox.Show(textLine);
                textBox10.AppendText(textLine);
                textBox10.AppendText(" ");
                textBox10.AppendText("\n");
                }
            fileInput4.Close();
            }

        private void button87_Click(object sender, EventArgs e)
            {
            textBox3.Text = textBox12.Text;
            }

        private void button76_Click(object sender, EventArgs e)
            {
            // Button: 独立权利
            rightfeaturecount = rightfeaturecount + 1;

            XDocument PatentTechRight = XDocument.Load("RightSpec.xml");
            XElement PatentTechRightRoot = PatentTechRight.Element("Root");
            XElement PatentTechRightF = new XElement("PatentRightCount",
                                                                                  new XAttribute("ID", rightfeaturecount.ToString()));

            PatentTechRightF.Add(new XElement("技术特征项", textBox50.Text));
            PatentTechRightF.Add(new XElement("前序部分", textBox51.Text));
            PatentTechRightF.Add(new XElement("特征部分", textBox52.Text));
            // TPatentTechMF.Add(new XElement("区别特征", textBox42.Text));
            // TPatentTechMF.Add(new XElement("技术优点", textBox43.Text));

            PatentTechRightRoot.Add(PatentTechRightF);

            PatentTechRight.Save("RightSpec.xml");

            textBox60.AppendText(textBox50.Text);
            textBox60.AppendText(",");
            textBox60.AppendText(textBox51.Text);
            textBox60.AppendText(",其特征在于:");
            textBox60.AppendText(textBox52.Text);
            textBox60.AppendText("\n");
            textBox60.AppendText("\n");
            
            textBox50.Text = "";
            textBox51.Text = "";
            textBox52.Text = "";

       
            }
           
        private void button88_Click(object sender, EventArgs e)
            {
            // Get  UnfinishedState from Unfinished.xml
            XDocument UnfinishedXml1 = XDocument.Load(InstallDirString +"\\Unfinished.xml");
            XElement   UnfinishedR1 = UnfinishedXml1.Element("Root");
            XAttribute  CurrentStateAttri = UnfinishedR1.Attribute("UnfinishedState");
            
           // int CurrentState = Convert.ToInt32(CurrentStateAttri);
            int CurrentState =0;
            if (Equals(CurrentState, 0))
                {
                dateTimePicker1.CustomFormat = "dd MMMM, yyyy-dddd";
                InventionDateDIR = InstallDirString + "\\Invention\\Invention" + dateTimePicker1.Value.ToLongDateString();
                textBox59.Text = InventionDateDIR;

                Directory.CreateDirectory(InventionDateDIR);
                Directory.CreateDirectory(CurrentDirString);

                // MOVE  \\StartupPath.xml ->Current.xml   AND Delete them
                // File.Move();

                }
            else
                {
                // MOVE  \\ Unfinished.xml -> \\Current.xml 

                Directory.Move(InstallDirString + "\\UnFinished", InstallDirString + "\\Current");

                }
                            
            }

        private void button6_Click(object sender, EventArgs e)
            {
            //Button: 保存技术交底书

            // Get InventionDateStr from CurrentPatentBasic.xml
            MessageBox.Show("保存技术交底书文档：技术交底书.txt");
            
            string CurrentFile1  =CurrentDirString+"\\技术交底书.txt";
            StreamWriter sw = new StreamWriter(CurrentFile1);
            MessageBox.Show(textBox1.Text);

            // write file according to line; add file format information "\n";
            // this need system approach to design this file format (change line)
           
            String PrimaryTechDocStr = textBox1.Text;
            sw.WriteLine(PrimaryTechDocStr);
            sw.Close();


            // Move Generated file into \\PrimaryTechDoc\\技术交底书.txt
            // UnFinished =1 
            XDocument UnFinishedDoc = XDocument.Load(InstallDirString +"\\Unfinished.xml");
            XElement UnFinishedRoot1 = UnFinishedDoc.Element("Root");
            UnFinishedRoot1.SetAttributeValue("UnFinishedState", 1);
            UnFinishedRoot1.Save("UnFinished.xml");
            }

        private void button74_Click(object sender, EventArgs e)
            {
            /*
            textBox60.AppendText("技术方案主题名称");
            textBox60.AppendText(",");
            textBox60.AppendText("发明主题与最接近现有技术共有的必要的技术特征");
            textBox60.AppendText(",其特征在于:");
            textBox60.AppendText("发明区别于最接近现有技术的技术特征");
            textBox60.AppendText("\n");
            */


            textBox50.Text = "技术方案主题名称";
            textBox51.Text = "发明主题与最接近现有技术共有的必要的技术特征";
            textBox52.Text = "发明区别于最接近现有技术的技术特征";
            }

        private void button79_Click(object sender, EventArgs e)
            {
            textBox55.Text = "技术特征项目名称";
            textBox54.Text = "(引用部分)根据权利要求编号所述+ 发明主题名称的方法";
            textBox53.Text = "(限定部分)该技术特征项目名称的技术特征";
            }

        private void button81_Click(object sender, EventArgs e)
            {
            textBox58.Text = "技术特征项目名称";
            textBox57.Text = "(引用部分)根据权利要求编号所述+ 发明主题名称的产品";
            textBox56.Text = "(限定部分)该技术特征项目名称的技术特征";
            }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser2 = new OpenFileDialog();
            DialogResult result = fileChooser2.ShowDialog();
            RightfileName1 = fileChooser2.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (RightfileName1 == " " || RightfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel1.Text = RightfileName1;
            }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser1 = new OpenFileDialog();
            DialogResult result = fileChooser1.ShowDialog();
            PatentfileName1 = fileChooser1.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (PatentfileName1 == " " || PatentfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel3.Text = PatentfileName1;
            }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser1 = new OpenFileDialog();
            DialogResult result = fileChooser1.ShowDialog();
            PatentfileName1 = fileChooser1.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (PatentfileName1 == " " || PatentfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel4.Text = PatentfileName1;
            }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser1 = new OpenFileDialog();
            DialogResult result = fileChooser1.ShowDialog();
            PatentfileName1 = fileChooser1.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (PatentfileName1 == " " || PatentfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel5.Text = PatentfileName1;
            }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            OpenFileDialog fileChooser1 = new OpenFileDialog();
            DialogResult result = fileChooser1.ShowDialog();
            PatentfileName1 = fileChooser1.FileName;
            if (result == DialogResult.Cancel)
                return;

            if (PatentfileName1 == " " || PatentfileName1 == null)
                MessageBox.Show("No File Exist");
            linkLabel6.Text = PatentfileName1;
            }

        private void button93_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "查看个人本体";
        }

        private void button95_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage12;
            tabControl3.SelectedTab = tabPage14;
            textBox2.Text = "";
            textBox2.Text = "将提示进展的电子邮件给发明者";
            textBox11.Text = textBox61.Text;


        }

        private void button92_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "使用当前TRIZ变换的启发式信息";
        }

        private void button91_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.Text = "提示培育层次和进展预先规划";
        }
    
   }
}
          
        
   

    
