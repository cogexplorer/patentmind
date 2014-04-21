// 
// Name:        PatentStatus.cs
//  Function:  Setup Patent Status and 
//  Author:       Zhuang. Chao

//  Date :        2012-1-25   
//  Date:         2012-3-18   Version 1.0
//  CopyRight:  遵循GPLv2版权协议, 开放源代码版权 2014
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatentMind
    {
    class PatentStatus
        {
        public string PatentName="";
        public string PatentNo="";
        private int PatentNumber = 1;
        public  LinkedStructure<string> PatentStatuslist = new LinkedStructure<string>();

        public void Init()
            {
        
            // Initialize
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "正式申请", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "格式审查", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "初审合格", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "文件公开", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "实质审查", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "审核通过", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "缴纳费用", "No"));
            PatentNumber++;
            PatentStatuslist.addData(new LinkedNode(PatentNumber, "专利权通过", "No"));

            }
        }
    }
