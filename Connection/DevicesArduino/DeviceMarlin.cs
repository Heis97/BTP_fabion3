﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class DeviceMarlin : DeviceArduino
    {
        public string port;
        int baudrate = 250000;
        int cur_com = 1;

        
        public DeviceMarlin(string _port) : base()
        {

            port = _port;
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;
            connect(port, baudrate);
            
        }

        int calcSum(string command)
        {
            int sum = 0;
            foreach (var symb in command) sum ^= symb;
            return sum;
        }


        public void sendCommand(string com,string[] vars, object[] vals)
        {
            if (vars.Length != vals.Length)
            {
                return;
            }
            var command = "N"+ cur_com.ToString()+ " "+ com;
            for(int i=0; i<vars.Length;i++)
            {
                command += " " + vars[i] + vals[i].ToString();
            }
            command += "*" + calcSum(command).ToString()+"\n";
            Console.WriteLine(command);
            serialPort.Write(command);
            cur_com++;
            var res_all = reseav();
            Console.WriteLine("res_all: "+res_all+" end");
            try
            {
                var res_all_arr = res_all.Split('\n');
                foreach(var res in res_all_arr)
                {
                    if (res.Contains("Resend"))
                    {

                        var ind_err = res.IndexOf("Resend");
                        var res_sub = res.Substring(ind_err);
                        var res_spl = res_sub.Split(':');
                       
                        var err = Convert.ToInt32(res_spl[1].Trim());
                        Console.WriteLine("err :" + err);
                        cur_com = err;
                        sendCommand(com, vars, vals);
                    }
                }
                
            }
            catch
            {

            }
            
        }

        void sendXpos(double pos)
        {
            sendCommand("M92", new string[] { "X"}, new object[] { 1 });
            sendCommand("G1",new string[] { "X","F" }, new object[] { pos,24000 });
        }


        public bool connectStart()
        {
            return connect(port, baudrate);
        }
        public void connectStop()
        {
            close();
        }

        public void setShvpPos(double _pos)
        {
            sendXpos(_pos);
        }

    }
}
