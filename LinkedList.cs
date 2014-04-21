
//
// Function: Basic Data Structure for Patent Content Analysis
// Author:   Zhuang, Chao
// Date:       2012-1-18
// Name:        LinkedList.cs
//  Function:  Processing  XML information
//  Author:       Zhuang. Chao  

//  Date          2012-1-25
//  Date          2012-3-18    Version 1.0
//  CopyRight:  遵循GPLv2版权协议, 开放源代码版权 2014
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatentMind
{
    class LinkedNode
    {
        public int Numberdata1;
        public string Namedata2;
        public string  Statusdata3;
        public LinkedNode  next;

        public LinkedNode(int  val1, string val2, string val3)    //   Node:=  No  + Name + Status + NextLink
        {
            Numberdata1 = val1;
            Namedata2 = val2;
            Statusdata3 = val3;
            next = null;
        }

      

        public LinkedNode Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

    }

    class LinkedStructure<T>
    {
        public  LinkedNode first;
        public  LinkedNode current;

        public LinkedStructure()
        {
            first = null;
        }

        public void addData(LinkedNode var)
        {
            if (first == null)
            {
                first = var;
                current = var;
                return;
            }
            else
            {
                current.Next = var;
                current = var;
            }
        }

      


        public void ChangeData(int number)
        {
            current = first; 
            while ((current != null) && (current.Numberdata1==number))
            {
                // Write the list to XML  Later
                current.Statusdata3 = "Yes";
                // WriteXML(   );
                current = current.Next;
            }
        //    WriteList2XML("PatentStatusXML.Xml");
        //    ChangeXML("PatentStatusXML.Xml");
        }
    }


    class   ListStruct
    {
      //  static void Main(string[] args)
       // {
       //     string s1 = "red ";
       //     string s2 = "blue ";
       //     string s3 = "yellow ";
       //     int no1 = 1;
      //      int no2 = 2;
       //     int no3 = 3;
            
      //      LinkedStructure<string> clist = new LinkedStructure<string>();

       //     clist.addData(new LinkedNode(no1,s1,s1));
       //     clist.addData(new LinkedNode(no2,s2,s2));
       //     clist.addData(new LinkedNode(no3,s3,s3));

        //      clist.displayData();    // Write the clist to the   5  types  XML
         
        //    Console.ReadLine();
        //  }
  }
        
     class  GetListData
            {
              LinkedStructure<string> Statuslist = new LinkedStructure<string> ();
              public int GetNoData(int number)
            {
                  int i1=1; 
                                 
                  LinkedNode var=Statuslist.first;
                  LinkedNode current = var;
                  while(i1<number)
                   { 
                      current = current.next;
                      var = current;
                      i1++;
                    }
                  return  var.Numberdata1;
            }

            public string  GetNameData(int number)
            {
                int i2=1;
                
                LinkedNode var = Statuslist.first;
                LinkedNode current = var;
                while (i2 <number)
                    {
                     current = current.next;
                     var = current;
                      i2++;
                    }
                return  var.Namedata2;
           
            }

            public string  GetStatusData(int number)
            {
                int i3=1;
                LinkedNode var=Statuslist.first;
                LinkedNode current = var;
                while (i3<number)
                    {
                      current = current.next;
                      var = current;
                     i3++;
                    }
                return  var.Statusdata3;
                
            }

          }
  
}
