using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutor.Core.Models
{
    public class SubjectOfInterestModel : ISubjectOfInterestModel
    {        
        public int Id { get; set; }
        public string SubjectTopics { get; set; }
        public string Note { get; set; }

        public string NoteToDisplay
        {
            get
            {
                string output = "";
                if (Note?.Length > 0)
                {
                    var noteSplit = Note.Split(' ');

                    for (int i = 0; i < 5; i++)
                    {
                        output += noteSplit[i] + " ";
                    }
                    if (output.Length > 50)
                    {
                        output = output.Remove(49, output.Length - 1);
                    }
                    output += "...";
                }
                return output;
            }
            
        }
        private string _subjectTopicsDisplay;

        public string SubjectTopicsDisplay
        {
            get 
            {
                _subjectTopicsDisplay = SubjectTopics;

                return _subjectTopicsDisplay; 
            }
            set 
            {
                _subjectTopicsDisplay = value; 
            }
        }


        private string _subjectTopicsDisplay1;
        public string SubjectTopicsDisplay1
        {
            get
            {
                if (_subjectTopicsDisplay1 == null)
                {
                    _subjectTopicsDisplay1 = SubjectTopics;
                }
                else if (_subjectTopicsDisplay1 == "Ch")
                {
                    _subjectTopicsDisplay1 = null;
                }
                return _subjectTopicsDisplay1;
            }
            set 
            {
                if (value == "")
                {
                    value = "Ch";
                }
                _subjectTopicsDisplay1 = value; 
            }
        }
        private string _subjectTopicsDisplay2;
        public string SubjectTopicsDisplay2
        {
            get
            {
                if (_subjectTopicsDisplay2 == null)
                {
                    _subjectTopicsDisplay2 = SubjectTopics;
                }
                else if (_subjectTopicsDisplay2 == "Ch")
                {
                    _subjectTopicsDisplay2 = null;
                }
                return _subjectTopicsDisplay2;
            }
            set 
            {
                if (value == "")
                {
                    value = "Ch";
                }
                _subjectTopicsDisplay2 = value;
            }
        }
        private string _subjectTopicsDisplay3;
        public string SubjectTopicsDisplay3
        {
            get
            {
                if (_subjectTopicsDisplay3 == null)
                {
                    _subjectTopicsDisplay3 = SubjectTopics;
                }
                else if (_subjectTopicsDisplay3 == "Ch")
                {
                    _subjectTopicsDisplay3 = null;
                }
                
                return _subjectTopicsDisplay3;
            }
            set 
            {
                if (value == "")
                {
                    value = "Ch";
                }
                _subjectTopicsDisplay3 = value;
            }
        } 
                       
    }

    public class SubjectOfInterestModelList : ISubjectOfInterestModelList
    {
        public List<SubjectOfInterestModel> SubjectOfInterestModels { get; set; } = new List<SubjectOfInterestModel>();
    }
}
