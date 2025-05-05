using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTP
{
    public class ConverterGCodeToACS
    {
        
        public ConverterGCodeToACS()
        {
            begin_part = File.ReadAllText(@"GcodeToAcs\begin.txt");
            end_part = File.ReadAllText(@"GcodeToAcs\end_short_2403.txt");
        }
        public string ConvertToAcs(string gCode)
        {
            var gCode_lines = gCode.Split('\n');

            string acs_commands = begin_part;
            string last_frame = ""; 
            foreach (var frame in gCode_lines)
            {
                if(!frame.Contains('%'))
                {
                    var acs_frame = convertFrame(frame);
                    if(acs_frame!=null)
                    {
                        acs_commands += acs_frame;
                    }
                    
                }  
            }
            acs_commands +=  end_part;
            return acs_commands;
        }
        public string ConvertToAcsShort(string gCode)
        {

            var gCode_lines = gCode.Split('\n');
            List<gFrame> frames = new List<gFrame>();
            string acs_commands = begin_part;
            foreach (var frame in gCode_lines)
            {
                if (!frame.Contains('%') && frame.Contains('G'))
                {
                    frames.Add(new gFrame(frame));
                }
            }
            acs_commands += generateGCode(frames.ToArray());
            acs_commands += end_part;
            return acs_commands;
        }
        string generateGCode(gFrame[] frames)
        {
            string g_code = "";
            for(int i=0;i<frames.Length-1;i++)
            {
                List<int> gA_res = new List<int>();
                List<double> gV_res = new List<double>();

                List<int> gA_ind = new List<int>();
                List<int> gV_ind = new List<int>();
                if(i == 0)
                {
                    for (int j = 0; j < 15; j++)
                    {
                            gA_res.Add(frames[i].gA[j]);
                            gA_ind.Add(j);
                    
                            gV_res.Add(frames[i].gV[j]);
                            gV_ind.Add(j);
                    }
                }
                else
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (frames[i - 1].gA[j] != frames[i].gA[j])
                        {
                            gA_res.Add(frames[i].gA[j]);
                            gA_ind.Add(j);
                        }
                        if (frames[i - 1].gV[j] != frames[i].gV[j])
                        {
                            gV_res.Add(frames[i].gV[j]);
                            gV_ind.Add(j);
                        }
                    }
                }
                
                //Console.WriteLine("i " + i);
                g_code +=printFrame(gA_res.ToArray(), gV_res.ToArray(), frames[i].gCom, gA_ind.ToArray(), gV_ind.ToArray());
            }
            return g_code;
        }
        string printFrame(int[] gA, double[] gV,int gCom, int[] gA_ind, int[] gV_ind)
        {

            string acsFrame = "";
            for (int i = 0; i < gA.Length; i++)
            {
                acsFrame += "gA(" + gA_ind[i].ToString() + ") = " +gA[i].ToString() + "; ";
            }
            for (int i = 0; i < gV.Length; i++)
            {
                acsFrame +="gV(" + gV_ind[i].ToString() + ") = " + gV[i].ToString() + "; ";
            }
            acsFrame += "call G" + gCom.ToString() + ";\n";
            return acsFrame;

        }
        string convertFrame_short(string g_codeFrame,string prev_frame)
        {
            if (g_codeFrame.Length < 2)
            {
                return null;
            }
            string acsFrame = "";
            var line_t = g_codeFrame.Trim();
            var line_s = line_t.Split(' ');
            int ind = 0;
            if (line_s.Length < 2)
            {
                return null;
            }

            for(int i=0;i< line_s.Length;i++)
            {
                string cor = line_s[i];
                if (cor.Length > 1)
                {
                    var symb = (int)cor[0];

                    acsFrame +=
                        "gA(" + ind.ToString() + ") = " + symb.ToString() + "; ";

                    var num = cor.Substring(1);
                    if (symb == 84)
                    {
                        num = "1";
                    }
                    acsFrame +=
                        "gV(" + ind.ToString() + ") = " + num + "; ";

                    ind++;
                }

            }
            var g_com_s = line_s[1];
            if (g_com_s.Length < 2)
            {
                return null;
            }
            var g_com_s1 = g_com_s.Substring(1);
            int g_com = Convert.ToInt32(g_com_s1);
            acsFrame += "call G" + g_com.ToString() + ";\n";
            return acsFrame;
        }
        string convertFrame(string g_codeFrame)
        {
            if(g_codeFrame.Length<2)
            {
                return null;
            }
            string acsFrame = "";
            var line_t = g_codeFrame.Trim();
            var line_s = line_t.Split(' ');
            int ind = 0;
            if(line_s.Length<2)
            {
                return null;
            }

            foreach( var cor in line_s)
            {
                if(cor.Length>1)
                {
                    var symb = (int)cor[0];
                    
                    acsFrame +=
                        "gA(" + ind.ToString() + ") = " + symb.ToString() + "; ";
                        
                    var num = cor.Substring(1);
                    if (symb == 84)
                    {
                        num = "1";
                    }
                    acsFrame +=
                        "gV(" + ind.ToString() + ") = " + num + "; ";

                    ind++;
                }
                
            }
            var g_com_s = line_s[1];
            if(g_com_s.Length<2)
            {
                return null;
            }
            var g_com_s1 = g_com_s.Substring(1);
            int g_com = Convert.ToInt32(g_com_s1);
            acsFrame += "call G"+g_com.ToString()+";\n";
            return acsFrame;
        }

        string begin_part = "";
        string end_part = "";

        
    }

    public struct gFrame
    {
        public int[] gA;
        public double[] gV;
        public int gCom;
        public gFrame(string str_frame)
        {
            gA = new int[40];
            gV = new double[40];
            var line_t = str_frame.Trim();
            var line_s = line_t.Split(' ');
            for (int i=2; i< line_s.Length;i++)
            {
                string cor = line_s[i];
                if (cor.Length > 1)
                {
                    gA[i] = (int)cor[0];
                    gV[i] = Convert.ToDouble(cor.Substring(1));
                    if(gA[i]==84)
                    {
                        gV[i] = 1;
                    }
                }
                //Console.WriteLine("g_i " + i);
            }
            var g_com_s = line_s[1];
            var g_com_s1 = g_com_s.Substring(1);
            gCom = Convert.ToInt32(g_com_s1);
        }
    }
}
