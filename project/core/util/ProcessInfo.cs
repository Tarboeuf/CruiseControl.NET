using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace ThoughtWorks.CruiseControl.Core.Util
{
	public class ProcessInfo
	{
		private const int DEFAULT_TIMEOUT = 120000;
		private ProcessStartInfo startInfo = new ProcessStartInfo();
		private string standardInputContent = null;

		public ProcessInfo(string filename) : this(filename, null) { }

		public ProcessInfo(string filename, string arguments) : this(filename, arguments, null) { }

		public ProcessInfo(string filename, string arguments, string workingDirectory)
		{
			startInfo.FileName = filename;
			startInfo.Arguments = arguments;
			startInfo.WorkingDirectory = workingDirectory;
			startInfo.UseShellExecute = false;
			startInfo.CreateNoWindow = true;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.RedirectStandardInput = false;
		}

		public StringDictionary EnvironmentVariables
		{
			get { return startInfo.EnvironmentVariables; }
		}

		public string FileName
		{
			get { return startInfo.FileName; }
		}

		public string Arguments 
		{
			get { return startInfo.Arguments; }
		}

		public string WorkingDirectory
		{
			get { return startInfo.WorkingDirectory; }
		}

		public string StandardInputContent
		{
			get { return standardInputContent; }
			set
			{
				startInfo.RedirectStandardInput = true;
				startInfo.UseShellExecute = false;
				standardInputContent = value;
			}
		}

		// Mostly for testing, otherwise its tricky due to the static call to 'Process.Start'
		public ProcessStartInfo StartInfo
		{
			get { return startInfo; }
		}

		public int TimeOut = DEFAULT_TIMEOUT;

		public Process CreateAndStartNewProcess()
		{
			return Process.Start(startInfo);
		}

		public override bool Equals(object obj)
		{
			ProcessInfo otherProcessInfo = obj as ProcessInfo;
			if (otherProcessInfo == null)
			{
				return false;
			}

			return (FileName == otherProcessInfo.FileName
				&& Arguments == otherProcessInfo.Arguments
				&& WorkingDirectory == otherProcessInfo.WorkingDirectory
				&& StandardInputContent == otherProcessInfo.StandardInputContent);
		}

		public override string ToString()
		{
			return string.Format("FileName: [{0}] -- Arguments: [{1}] -- WorkingDirectory: [{2}] -- StandardInputContent: [{3}] ",
				FileName, Arguments, WorkingDirectory, StandardInputContent);
		}

	}
}
