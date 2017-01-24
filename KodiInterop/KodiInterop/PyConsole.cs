﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smx.KodiInterop
{
    public static class PyConsole
    {
		private static string NewLine {
			get	{
				if(Environment.NewLine.Length > 1) {
					return @"\r\n";
				} else {
					return @"\n";
				}
			}
		}

		/// <summary>
		/// Writes the text representation of the specified object to kodi.log with "print"
		/// </summary>
		/// <param name="value"></param>
		public static void Print(object value) {
			string valueStr = PythonInterop.EscapeArgument(value.ToString());
			PythonInterop.Eval(string.Format("print {0}", valueStr));
		}

		public static void FancyPrint(string value) {
			string printStr = NewLine + NewLine;
			for (int i = 0; i < value.Length + 4; i++)
				printStr += '-';

			printStr += "| " + value + " |" + NewLine;

			for (int i = 0; i < value.Length + 4; i++)
				printStr += '-' + NewLine;

			Write(printStr);
		}

		public static void WriteLine(string value, bool escape = true) {
			string valueStr = value;
			if (escape)
				valueStr = PythonInterop.EscapeArgument(valueStr);
			valueStr += PythonInterop.EscapeArgument(NewLine, quote: true, rawstr: false);
			PythonInterop.Eval(string.Format("sys.stdout.write({0})", valueStr));
		}

		/// <summary>
		/// Writes the text representation of the specified object to kodi.log with "sys.stdout.write"
		/// </summary>
		/// <param name="value"></param>
		public static void Write(object value, bool escape = true) {
			string valueStr = value.ToString();
			if (escape)
				valueStr = PythonInterop.EscapeArgument(valueStr);

			PythonInterop.Eval(string.Format("sys.stdout.write({0})", valueStr));
		}
	}
}
