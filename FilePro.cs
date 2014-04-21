/ 
// Name:        FilePro.cs
//  Function:   Processing  XML information
//  Author:     Zhuang. Chao

//  Date        2012-1-25
//
//  XML :       CurrentPatentBasic.Xml
//  XML:        TechFeature.Xml
//  XML:        RightSpec.Xml

//  Date:       2012-3-18    Version 1.0
// 
//  CopyRight:  遵循GPLv2版权协议, 开放源代码版权 2014
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace PatentMind
    {
    class Filepro
        {
        //Process Data Information


          public void GetCurrentPatent()
                {


                }
        // process XML
          public void  SetupPatentXML()
                 {

                   PatentStatus PatentListS1 = new PatentStatus();
                    PatentListS1.Init();                      // Later change the patentlist and Set up XML 

                    XDocument PatentBasic = XDocument.Load("CurrentPatentBasic.xml");
                    XElement ePatent = PatentBasic.Element("Root");
                    XElement PatentName = ePatent.Element("PatentName");
                    XElement PatentAppNo = ePatent.Element("PatentAppNo");
                    XElement PatentNo = ePatent.Element("PatentNo");
                   
              //    GetPatentName(PatentName);
                //  GetPatentNo (PatentNo);
                   
            
                  XDocument  PatentStatusXml = new XDocument(
                                   new XElement("Root"));

                 PatentStatusXml.Add(new XElement ("PatentName",PatentName));
                 PatentStatusXml.Add(new XElement("PatentAppNo",PatentAppNo));
                 PatentStatusXml.Add(new XElement ("PatentNo",PatentNo));

                 LinkedNode current = PatentListS1.PatentStatuslist.first;
                 GetListData getData = new GetListData();

                 for (int i =1;i<=8; i++)
                     {
                          PatentStatusXml.Add(new XElement("StatusItem"+i.ToString(),
                                                               new XElement ("Number", getData.GetNoData(i)).ToString()),
                                                               new XElement ("Name",  getData.GetNameData(i)),
                                                               new XElement( "Status", getData.GetStatusData(i)));
                          current = current.next;
                     }
         
                 PatentStatusXml.Save(PatentName+".xml");
                  
            }

        
        public void ChangePatentXMLStatus(string PatentName)
           {
            //  GetChangeStatus Infomation
                 SetupPatentXML();
                 XDocument xPatentXML = XDocument.Load(PatentName+".xml");

                 XElement rtPatentXML = xPatentXML.Element("Root");
               

                 for (int j = 1; j <= 8; j++)
                     {
                        XElement rtPatentStatus = rtPatentXML.Element("StatusItem"+j.ToString());
                            


                     }
                 
                 
            }
        }
    }
