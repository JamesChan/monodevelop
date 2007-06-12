//
// ResourceFileDescriptionTemplate.cs
//
// Author:
//   Lluis Sanchez Gual
//
// Copyright (C) 2006 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using System.Xml;
using MonoDevelop.Ide.Projects;

namespace MonoDevelop.Ide.Templates
{
	public class ResourceFileDescriptionTemplate: FileDescriptionTemplate
	{
		SingleFileDescriptionTemplate template;
		
		public override string Name { 
			get { return template.Name; } 
		}
		
		public override void Load (XmlElement filenode)
		{
			foreach (XmlNode node in filenode.ChildNodes) {
				if (node is XmlElement) {
					template = CreateTemplate ((XmlElement) node) as SingleFileDescriptionTemplate;
					if (template == null)
						throw new InvalidOperationException ("Resource templates must contain single-file templates.");
					return;
				}
			}
		}
		
		public override string[] Create (string language, string directory, string name)
		{
			return template.Create (language, directory, name);
		}
		
		public override void AddToProject (IProject project, string language, string directory, string name)
		{
			project.Add (new ProjectFile( Path.Combine (directory, name), FileType.EmbeddedResource));
		}
		
		public override void Show ()
		{
			template.Show ();
		}
	}
}
