using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//
// IniFile class 
// by Todd Davis (toddhd@hotmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// This permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace JHDock
{

    public class IniFile
    {
        private ArrayList Sections = new ArrayList();
        public string CommentString = ";";

        private string _FileName;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Returns the contents of the IniFile in a text or HTML format
        /// </summary>
        /// <param name="ReturnAsHTML">Optional, defaults to false. Carriage return/linefeeds are converted to HTML.</param>
        /// <value></value>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public string get_Text([System.Runtime.InteropServices.Optional] bool ReturnAsHTML/* TRANSERROR: default parameter value: false */  )
        {
            return GetText(ReturnAsHTML);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Contructor called when creating a new IniFile. Requires a filename value.
        /// </summary>
        /// <param name="FileName">The path to the file to be edited.</param>
        /// <param name="CreateIfNotExist">Optional, defaults to true. If the file does not exist, it is created.</param>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public IniFile(string FileName, bool CreateIfNotExist = true)
        {
            Read(FileName, CreateIfNotExist);
            _FileName = FileName;
        }

        private bool Read(string Filename, bool CreateIfNotExist = true)
        {
            // verify that the file exists
            if (!File.Exists(Filename))
            {
                //if it does not exist, check to see if we should create it
                if (CreateIfNotExist)
                {
                    try
                    {
                        FileStream fs = File.Create(Filename);
                        //create it
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        Interaction.MsgBox("Error: Cannot create file " + Filename + Constants.vbCrLf + ex.ToString(), MsgBoxStyle.Critical, "Error creating IniFile");
                        return false;
                    }
                }
                else
                {
                    Interaction.MsgBox("Error: File " + Filename + " does not exist. Cannot create IniFile.", MsgBoxStyle.Critical, "Critical Error");
                    return false;
                }
            }

            Sections.Clear();
            //Clear the arraylist

            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Filename);
                //Declare a streamreader
                string CurrentSection = "";
                //Flag to track what section we are currently in
                string ThisLine = sr.ReadLine();
                //Read in the first line

                do
                {
                    switch (Eval(ThisLine))
                    {
                        //Evalue the contents of the line
                        case "Section":
                            AddSection(RemoveBrackets(RemoveComment(ThisLine)), IsCommented(ThisLine));
                            //Add the section to the sections arraylist
                            CurrentSection = RemoveBrackets(RemoveComment(ThisLine));
                            //Make this the current section, so we know where to keys to
                            break;
                        case "Key":
                            AddKey(GetKeyName(ThisLine), GetKeyValue(ThisLine), CurrentSection, IsCommented(ThisLine));
                            break;
                        case "Comment":
                            break;
                        //AddComment(ThisLine, CurrentSection)
                        case "Blank":
                            break;
                        //TODO: Should we create a blank object to handle blanks?
                        case "":
                            break;
                        //We hit something unknown - ignore it
                    }
                    ThisLine = sr.ReadLine();
                    //Get the next line
                } while (!(ThisLine == null));
                //continue until the end of the file
                sr.Close();
                //close the file

                return true;

            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error: " + ex.ToString(), MsgBoxStyle.Critical, "Error");
                return false;
            }


        }

        private string Eval(string value)
        {
            value = Strings.Trim(value);
            //Remove any leading/trailing spaces, just in case they exist

            //If the value is blank, then it is a blank line
            if (string.IsNullOrEmpty(value))
                return "Blank";

            //If the value is surrounded by brackets, then it is a section
            if (Strings.Left(RemoveComment(value), 1) == "[" & Strings.Right(value, 1) == "]")
                return "Section";

            //If the value contains an equals sign (=), then it is a value. This test can be fooled by 
            //comment with an equals sign in it, but it is the best test we have. We test for this before
            //testing for a comment in case the key is commented out. It is still a key.
            if (Strings.InStr(value, "=", CompareMethod.Text) > 0)
                return "Key";

            //If the value is preceeded by the comment string, then it is a pure comment
            if (IsCommented(value))
                return "Comment";

            return "";
        }

        private string GetKeyName(string Value)
        {
            //If the value is commented out, then remove the comment string so we can get the name
            if (IsCommented(Value))
                Value = RemoveComment(Value);
            int Equals = Strings.InStr(Value, "=", CompareMethod.Text);
            //Locate the equals sign
            //It should be, but just to be safe
            if (Equals > 0)
            {
                return Strings.Left(Value, Equals - 1);
                //Return everything before the equals sign
            }
            else
            {
                return "";
            }
        }

        private string GetKeyValue(string value)
        {
            int Equals = Strings.InStr(value, "=", CompareMethod.Text);
            //Locate the equals sign
            //It should be, but just to be safe
            if (Equals > 0)
            {
                return Strings.Right(value, Strings.Len(value) - Equals);
                //Return everything after the equals sign
            }
            else
            {
                return "";
            }
        }

        private bool IsCommented(string value)
        {
            //Return true if the passed value starts with a comment string
            if (Strings.Left(value, Strings.Len(CommentString)) == CommentString)
                return true;
            return false;
        }

        private string RemoveComment(string value)
        {
            //Return the value with the comment string stripped
            return (IsCommented(value) ? value.Remove(0, Strings.Len(CommentString)) : value);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a key/value to a given section. If the section does not exist, it is created.
        /// </summary>
        /// <param name="KeyName">The name of the key to add. If the key alreadys exists, then no action is taken.</param>
        /// <param name="KeyValue">The value to assign to the new key.</param>
        /// <param name="SectionName">The section to add the new key to. If it does not exist, it is created.</param>
        /// <param name="IsCommented">Optional, defaults to false. Will create the key in commented state.</param>
        /// <param name="InsertBefore">Optional. Will insert the new key prior to the specified key.</param>
        /// <returns></returns>
        /// <remarks>If the section does not exist, it will be created. If the 'IsCommented' option is true, then the newly created section will also be commented. If the 'InsertBefore' option is used, the specified key does not exist, then the new key is simply added to the section. If the section the key is being added to is commented, then the key will be commented as well.
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool AddKey(string KeyName, string KeyValue, string SectionName, bool IsCommented = false, string InsertBefore = null)
        {
            Section ThisSection = GetSection(SectionName);
            //verify that the section exists
            if (ThisSection == null)
            {
                AddSection(SectionName, IsCommented);
                ThisSection = GetSection(SectionName);
            }
            if (ThisSection.IsCommented)
                IsCommented = true;
            //If the section is commented out, then this key must be too
            if ((GetKey(KeyName, SectionName) != null))
                return false;
            //verify that the key does *not* exist
            Key ThisKey = new Key(KeyName, KeyValue, IsCommented);
            //create a new key
            //if no insertbefore is required
            if (InsertBefore == null)
            {
                ThisSection.Add(ThisKey);
                //then add the new key to the bottom of the section
                return true;
            }
            else
            {
                object KeyIndex = GetKeyIndex(InsertBefore, SectionName); // locate the key to insert prior to
                if (System.Convert.ToDouble(KeyIndex) > -1)
                { // if the key exists

                    ThisSection.Insert(System.Convert.ToInt32(KeyIndex), ThisKey); // then do the insert
                    return true;
                }
                else
                {
                    ThisSection.Add(ThisKey); // the key to insert prior to wasn't found, so just add it
                    return false; // the key to insert prior to was not found
                } 
                //dynamic KeyIndex = GetKeyIndex(InsertBefore, SectionName);
                ////locate the key to insert prior to
                ////if the key exists
                //if (KeyIndex > -1)
                //{
                //    ThisSection.Insert(KeyIndex, ThisKey);
                //    //then do the insert
                //    return true;
                //}
                //else
                //{
                //    ThisSection.Add(ThisKey);
                //    //the key to insert prior to wasn't found, so just add it
                //    return false;
                //    //the key to insert prior to was not found
                //}
            }
        }

        private int GetKeyIndex(string KeyName, string SectionName)
        {
            //returns the index of a given key
            //Dim ThisKey As Key = GetKey(KeyName, SectionName)
            //If ThisKey Is Nothing Then Return -1
            //Dim ThisSection As Section = GetSection(SectionName) 
            //Return ThisSection.IndexOf(ThisKey.Name)

            System.Collections.IEnumerator SectionEnumerator = Sections.GetEnumerator();
            while (SectionEnumerator.MoveNext())
            {
                if (((Key)SectionEnumerator.Current).Name == SectionName)
                {
                    System.Collections.IEnumerator KeyEnumerator = ((ArrayList)SectionEnumerator.Current).GetEnumerator();
                    while (KeyEnumerator.MoveNext())
                    {
                        if (((Key)KeyEnumerator.Current).Name == KeyName)
                        {
                            return ((ArrayList)SectionEnumerator.Current).IndexOf(KeyEnumerator.Current);
                        }
                    }
                } 
                //if (SectionEnumerator.Current.Name == SectionName)
                //{
                //    System.Collections.IEnumerator KeyEnumerator = SectionEnumerator.Current.GetEnumerator();
                //    while (KeyEnumerator.MoveNext())
                //    {
                //        if (KeyEnumerator.Current.Name == KeyName)
                //            return SectionEnumerator.Current.indexof(KeyEnumerator.Current);
                //    }
                //}
            }
            return -1;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a section to the IniFile. If the section already exists, then no action is taken.
        /// </summary>
        /// <param name="SectionName">The name of the section to add.</param>
        /// <param name="IsCommented">Optional, defaults to false. Will add the section in a commented state.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public object AddSection(string SectionName, bool IsCommented = false)
        {
            if (GetSection(SectionName) == null)
                Sections.Add(new Section(SectionName, IsCommented));
            //Add the section to the sections arraylist
            return null;
        }

        public Section GetSection(string SectionName)
        {
            //Return the given section object
            System.Collections.IEnumerator myEnumerator = Sections.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                Section CurrentSection = ((JHDock.Section)(myEnumerator.Current));
                if (  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ CurrentSection.Name.ToLower() ==  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ SectionName.ToLower())
                {
                    return ((JHDock.Section)(myEnumerator.Current));
                } 
                //Section CurrentSection = myEnumerator.Current;
                //if (Strings.LCase(CurrentSection.Name) == Strings.LCase(SectionName))
                //    return myEnumerator.Current;
            }
            return null;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Return the sections in the IniFile.
        /// </summary>
        /// <returns>Returns an ArrayList of Section objects.</returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public ArrayList GetSections()
        {
            //returns an arraylist of the sections in the inifile
            ArrayList ListOfSections = new ArrayList();
            System.Collections.IEnumerator myEnumerator = Sections.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                ListOfSections.Add(myEnumerator.Current);
            }
            return ListOfSections;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Returns an arraylist of Key objects in a given Section. Section must exist.
        /// </summary>
        /// <param name="SectionName">The name of the Section to retrieve the keys from.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public ArrayList GetKeys(string SectionName)
        {
            //returns an arraylist of the keys in a given section
            ArrayList ListOfKeys = new ArrayList();
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return null;
            System.Collections.IEnumerator KeyEnumerator = ThisSection.GetEnumerator();
            while (KeyEnumerator.MoveNext())
            {
                ListOfKeys.Add(KeyEnumerator.Current);
            }

            return ListOfKeys;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comments a given section, including all of the keys contained in the section.
        /// </summary>
        /// <param name="SectionName">The name of the Section to comment.</param>
        /// <returns></returns>
        /// <remarks>Keys that are already commented will <b>not</b> preserve their comment status if 'UnCommentSection' is used later on.
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool CommentSection(string SectionName)
        {
            //Comments a given section and all of its keys
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            ThisSection.IsCommented = true;
            System.Collections.IEnumerator myEnumerator = ThisSection.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                //myEnumerator.Current.IsCommented = true;
                ((Key)myEnumerator.Current).IsCommented = true; 
            }
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Uncomments a given section, and all of its keys.
        /// </summary>
        /// <param name="SectionName">The name of the Section to uncomment.</param>
        /// <returns></returns>
        /// <remarks>Any keys in the section that were previously commented will be uncommented after this function.
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool UnCommentSection(string SectionName)
        {
            //Uncomments a given section and all of its keys
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            ThisSection.IsCommented = false;
            System.Collections.IEnumerator myEnumerator = ThisSection.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                //myEnumerator.Current.IsCommented = false;
                ((Key)myEnumerator.Current).IsCommented = false; 
            }
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comments a given key in a given section. Both the key and the section must exist. 
        /// </summary>
        /// <param name="KeyName">The name of the key to comment.</param>
        /// <param name="SectionName">The name of the section the key is in.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool CommentKey(string KeyName, string SectionName)
        {
            //Comments a given a key
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            ThisKey.IsCommented = true;
            return false;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Uncomments a given key in a given section. Both the key and section must exist.
        /// </summary>
        /// <param name="KeyName">The name of the key to uncomment.</param>
        /// <param name="SectionName">The name of the section the key is in.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool UnCommentKey(string KeyName, string SectionName)
        {
            //Uncomments a given key
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            ThisKey.IsCommented = false;
            return false;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Renames a section. The section must exist.
        /// </summary>
        /// <param name="SectionName">The name of the section to be renamed.</param>
        /// <param name="NewSectionName">The new name of the section.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool RenameSection(string SectionName, string NewSectionName)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            ThisSection.Name = NewSectionName;
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Renames a given key key in a given section. Both they key and the section must exist. The value is not altered.
        /// </summary>
        /// <param name="KeyName">The name of the key to be renamed.</param>
        /// <param name="SectionName">The name of the section the key exists in.</param>
        /// <param name="NewKeyName">The new name of the key.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool RenameKey(string KeyName, string SectionName, string NewKeyName)
        {
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            ThisKey.Name = NewKeyName;
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Changes the value of a given key in a given section. Both the key and the section must exist.
        /// </summary>
        /// <param name="KeyName">The name of the key whose value should be changed.</param>
        /// <param name="SectionName">The name of the section the key exists in.</param>
        /// <param name="NewValue">The new value to assign to the key.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public object ChangeValue(string KeyName, string SectionName, string NewValue)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            ThisKey.Value = NewValue;
            return true;
        }

        public Key GetKey(string KeyName, string SectionName)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return null;
            System.Collections.IEnumerator myEnumerator = ThisSection.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                if (  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ ((Key)myEnumerator.Current).Name.ToLower() ==  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ KeyName.ToLower())
                {
                    return ((JHDock.Key)(myEnumerator.Current));
                } 
                //if (Strings.LCase(myEnumerator.Current.Name) == Strings.LCase(KeyName))
                    //return myEnumerator.Current;
            }
            return null;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deletes a given section. The section must exist. All the keys in the section will also be deleted.
        /// </summary>
        /// <param name="SectionName">The name of the section to be deleted.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool DeleteSection(string SectionName)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            Sections.Remove(ThisSection);
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Deletes a given key in a given section. Both the key and the section must exist.
        /// </summary>
        /// <param name="KeyName">The name of the key to be deleted.</param>
        /// <param name="SectionName">The name of the section the key exists in.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool DeleteKey(string KeyName, string SectionName)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            ThisSection.Remove(ThisKey);
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Moves a key from one section to another. Both the key and the section must exist, as must the section to move the key to.
        /// </summary>
        /// <param name="KeyName">The name of the key to be moved.</param>
        /// <param name="SectionName">The name of the section the key exists in.</param>
        /// <param name="NewSectionName">The name of the section to move the key to.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool MoveKey(string KeyName, string SectionName, string NewSectionName)
        {
            Section ThisSection = GetSection(SectionName);
            if (ThisSection == null)
                return false;
            Section ThisNewSection = GetSection(NewSectionName);
            if (ThisNewSection == null)
                return false;
            Key ThisKey = GetKey(KeyName, SectionName);
            if (ThisKey == null)
                return false;
            if ((GetKey(KeyName, NewSectionName) != null))
                return false;
            //Verifiy that the key doesn't already exist in the new section
            ThisSection.Remove(ThisKey);
            ThisNewSection.Add(ThisKey);
            return true;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Sorts all of the sections, and all of the keys within the sections.
        /// </summary>
        /// <returns></returns>
        /// <remarks>There is no undo feature for this operation.
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public object Sort()
        {
            SectionComparer mySC = new SectionComparer();
            Sections.Sort(mySC);
            System.Collections.IEnumerator myEnumerator = Sections.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                //myEnumerator.Current.Sort(mySC);
                ((ArrayList)myEnumerator.Current).Sort(mySC); 
            }
            return null;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Saves the IniFile to the specified filename.
        /// </summary>
        /// <param name="FileName">The filename to save the inifile to.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public object Save(string FileName)
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            // Remove the existing file

            //Loop through the arraylist (Content) and write each line to the file
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName);

            System.Collections.IEnumerator SectionEnumerator = Sections.GetEnumerator();
            while (SectionEnumerator.MoveNext())
            {
                Section kk = (Section)SectionEnumerator.Current;
                sw.Write(AddBrackets(kk.Name) + "\r\n");
                //sw.Write(AddBrackets(((Key)SectionEnumerator.Current).Name) + "\r\n");
                System.Collections.IEnumerator KeyEnumerator = ((ArrayList)SectionEnumerator.Current).GetEnumerator();
                while (KeyEnumerator.MoveNext())
                {
                    sw.Write(((Key)KeyEnumerator.Current).Name + "=" + ((Key)KeyEnumerator.Current).Value + "\r\n");
                } 
                //sw.Write(AddBrackets(SectionEnumerator.Current.Name) + Constants.vbCrLf);
                //System.Collections.IEnumerator KeyEnumerator = SectionEnumerator.Current.GetEnumerator();
                //while (KeyEnumerator.MoveNext())
                //{
                //    sw.Write(KeyEnumerator.Current.Name + "=" + KeyEnumerator.Current.Value + Constants.vbCrLf);
                //}
            }
            sw.Close();
            return null;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Saves the inifile to the specified filename in XML format. 
        /// </summary>
        /// <param name="FileName">The name of the file to save the inifile to.</param>
        /// <param name="Encode">Optional, defaults to nothing. May pass an encoding object (such as UTF-8) here.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public object SaveXML(string FileName, Encoding Encode = null)
        {
            string strXMLPath = FileName;
            XmlTextWriter objXMLWriter = null;
            objXMLWriter = new XmlTextWriter(strXMLPath, Encode);
            //Create a new XML file

            objXMLWriter.WriteStartDocument();
            objXMLWriter.WriteStartElement("configuration");

            System.Collections.IEnumerator SectionEnumerator = Sections.GetEnumerator();
            while (SectionEnumerator.MoveNext())
            {
                Key key = (Key)SectionEnumerator.Current;
                objXMLWriter.WriteStartElement("section");
                objXMLWriter.WriteAttributeString("name", key.Name);
                System.Collections.IEnumerator KeyEnumerator = ((ArrayList)SectionEnumerator.Current).GetEnumerator();
                while (KeyEnumerator.MoveNext())
                {
                    objXMLWriter.WriteStartElement("setting");
                    objXMLWriter.WriteAttributeString("name", key.Name);
                    objXMLWriter.WriteAttributeString("value", key.Value);
                    objXMLWriter.WriteEndElement();
                }
                objXMLWriter.WriteEndElement(); 
                //objXMLWriter.WriteStartElement("section");
                //objXMLWriter.WriteAttributeString("name", SectionEnumerator.Current.Name);
                //System.Collections.IEnumerator KeyEnumerator = SectionEnumerator.Current.GetEnumerator();
                //while (KeyEnumerator.MoveNext())
                //{
                //    objXMLWriter.WriteStartElement("setting");
                //    objXMLWriter.WriteAttributeString("name", KeyEnumerator.Current.Name);
                //    objXMLWriter.WriteAttributeString("value", KeyEnumerator.Current.Value);
                //    objXMLWriter.WriteEndElement();
                //}
                //objXMLWriter.WriteEndElement();
            }

            objXMLWriter.WriteEndElement();
            //write the ending tag for configuration
            objXMLWriter.WriteEndDocument();
            objXMLWriter.Flush();
            objXMLWriter.Close();
            return null;
        }

        private string GetText(bool ReturnAsHTML = false)
        {
            string CrLf = (ReturnAsHTML ? "<br>" : Constants.vbCrLf);
            StringBuilder sb = new StringBuilder();
            System.Collections.IEnumerator SectionEnumerator = Sections.GetEnumerator();
            while (SectionEnumerator.MoveNext())
            {
                sb.Append(AddBrackets(((Key)SectionEnumerator.Current).Name) + CrLf);
                System.Collections.IEnumerator KeyEnumerator = ((ArrayList)SectionEnumerator.Current).GetEnumerator();
                while (KeyEnumerator.MoveNext())
                {
                    sb.Append(((Key)KeyEnumerator.Current).Name + "=" + ((Key)KeyEnumerator.Current).Value + CrLf);
                } 
                //sb.Append(AddBrackets(SectionEnumerator.Current.Name) + CrLf);
                //System.Collections.IEnumerator KeyEnumerator = SectionEnumerator.Current.GetEnumerator();
                //while (KeyEnumerator.MoveNext())
                //{
                //    sb.Append(KeyEnumerator.Current.Name + "=" + KeyEnumerator.Current.Value + CrLf);
                //}
            }
            return sb.ToString();
        }

        private string RemoveBrackets(string Value)
        {
            char[] chArr = { char.Parse("["), char.Parse("]"), char.Parse(" ") }; 
        //    char[] chArr = {
        //    "[",
        //    "]",
        //    " "
        //};
            Value = Value.TrimStart(chArr);
            Value = Value.TrimEnd(chArr);
            return Value;
        }

        private string AddBrackets(string Value)
        {
            return "[" + Strings.Trim(Value) + "]";
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Saves the top, left, width & height properties of a form to the inifile
        /// </summary>
        /// <param name="WinForm">The windows form whose properties you wish to save.</param>
        /// <returns></returns>
        /// <remarks>Creates a section called '[Form <FormName> Settings]', with top, left, width and height keys
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool SaveFormSettings(Form WinForm)
        {
            if ((WinForm != null))
            {
                string SectionName = "Form " + WinForm.Name + " Settings";
                AddSection(SectionName);
                AddKey("Top", WinForm.Top.ToString(), SectionName);
                AddKey("Left", WinForm.Left.ToString(), SectionName);
                AddKey("Width", WinForm.Width.ToString(), SectionName);
                AddKey("Height", WinForm.Height.ToString(), SectionName);
                Save(_FileName);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the stored values of a form previously saved with funciton SaveFormSettings
        /// </summary>
        /// <param name="WinForm">The form object to restore the values to.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [TDavis]    1/19/2004    Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool GetFormSettings(ref Form WinForm)
        {
            string SectionName = "Form " + WinForm.Name + " Settings";
            if ((GetSection(SectionName) != null))
            {
                if (!(GetKey("Top", SectionName) == null))
                {
                    WinForm.Top = System.Convert.ToInt32(double.Parse(GetKey("Top", SectionName).Value));
                }
                if (!(GetKey("Left", SectionName) == null))
                {
                    WinForm.Left = System.Convert.ToInt32(double.Parse(GetKey("Left", SectionName).Value));
                }
                if (!(GetKey("Width", SectionName) == null))
                {
                    WinForm.Width = System.Convert.ToInt32(double.Parse(GetKey("Width", SectionName).Value));
                }
                if (!(GetKey("Height", SectionName) == null))
                {
                    WinForm.Height = System.Convert.ToInt32(double.Parse(GetKey("Height", SectionName).Value));
                } 
                //if ((GetKey("Top", SectionName) != null))
                //    WinForm.Top = GetKey("Top", SectionName).Value;
                //if ((GetKey("Left", SectionName) != null))
                //    WinForm.Left = GetKey("Left", SectionName).Value;
                //if ((GetKey("Width", SectionName) != null))
                //    WinForm.Width = GetKey("Width", SectionName).Value;
                //if ((GetKey("Height", SectionName) != null))
                //    WinForm.Height = GetKey("Height", SectionName).Value;
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}