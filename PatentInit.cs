// 
// Name:        PatentInit.cs
//  Function:  Set Current Patent  XML information
//  Author:     Zhuang. Chao

//  Date          2012-1-25
//
//  XML:       TechFeature.Xml                   .......Tech Feature Name 
//  XML:       RightSpec.Xml                       .......RIGHT
//  XML:       CurrentPatentBasic.Xml        ...... Application
//
//  Date:       2012-3-18    Version 1.0
//  CopyRight:  遵循GPLv2版权协议, 开放源代码版权 2014
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace PatentMind
    {

    class PatentInit
        {
        public void Init()
          {  // Set Up Patent Content Directory，Using CurrentPatentBasic.xml  for  CurrentPatent Information
            
            /*  Later delete file in \\Current Directory
            FileInfo FInfo1 = new FileInfo("CurrentPatentBasic.xml");
            if  (FInfo1.Exists)   File.Delete("CurrentPatentBasic.xml");

            FileInfo FInfo2 = new FileInfo("RightSpec.xml");
            if  (FInfo2.Exists)   File.Delete("RightSpec.xml");
            
            FileInfo FInfo3 = new FileInfo("MethodSpec.xml");
            if (FInfo3.Exists) File.Delete("MethodSpec.xml");

            FileInfo FInfo4 = new FileInfo("TechFeature.xml");
            if (FInfo4.Exists) File.Delete("TechFeature.xml");

            FileInfo FInfo5 = new FileInfo("DeviceSpec.xml");
            if (FInfo5.Exists) File.Delete("DeviceSpec.xml");
            */

            //  current editing patent information
            //  Using TechFeature.XML  and RightSpec.XML
            XDocument TechFeatureDoc = new XDocument(new XElement("Root"));
            XDocument RightSpecDoc = new XDocument(new XElement("Root"));
            XDocument MethodSpecDoc = new XDocument(new XElement("Root"));
            XDocument DeviceSpecDoc = new XDocument(new XElement("Root"));
            XDocument CurrentPatentBasic = new XDocument(new XElement("Root"));
            
             // Current Date Directory 
            //  Get InventionDateDIR from CurrentPatentBasic.xml
    
            FileInfo FInfo1 = new FileInfo("Unfinished.xml");
            if  ( !(FInfo1.Exists))   
                {
                  XDocument UnfinishedDoc = new XDocument(new XElement("Root",
                                                                                     new XAttribute("UnFinishedState", "0")));
                  UnfinishedDoc.Save("Unfinished.xml");
                }
            
            FileInfo FInfo2 = new FileInfo("finished.xml");
            if (!(FInfo2.Exists))
                {

                 XDocument finishedDoc = new XDocument(new XElement("Root",
                                                                                        new XAttribute("FinishedState", "0")));

                finishedDoc.Save("finished.xml");
                } 
            CurrentPatentBasic.Save("CurrentPatentBasic.xml");
            TechFeatureDoc.Save("TechFeature.xml");
            RightSpecDoc.Save("RightSpec.xml");
            MethodSpecDoc.Save("MethodSpec.xml");
            DeviceSpecDoc.Save("DeviceSpec.xml");

            }
        }
    }
