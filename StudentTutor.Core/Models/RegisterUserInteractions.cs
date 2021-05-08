using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudentTutor.Core.Models
{
    public class FileDialogInteraction
    {
        public Action<bool> SelectedFile { get; set; }
        public FileInfo File { get; set; }
    }
    public class RemoveSelectedSubjectInteraction
    {
        public Action<bool> SubjectSelected { get; set; }
    }
}
