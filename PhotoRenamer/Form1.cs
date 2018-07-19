using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoRenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            List<string> myPics = new List<string>();
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\myCDKview.jpg");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\IMG_2750.JPG");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\surge-Amanda.jpg");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\myCDKview - Copy.jpg");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\IMG_2750 - Copy.JPG");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\surge-Amanda - Copy.jpg");
            myPics.Add(@"C:\GitHub\PhotoRenamer\PhotoRenamer\surge-Amanda - Copy2.jpg");

            //string dirScan = @"C:\GitHub\PhotoRenamer\PhotoRenamer\files2.txt";
            string dirScan = tbDirScan.Text;

            List<string> allLinesText = File.ReadAllLines(dirScan).ToList();
            int dupCounter = 0;

            //foreach (var item in myPics)
            foreach (var item in allLinesText)
            {
                FileInfo info = new FileInfo(item);
                string fullName = item.ToString();
                string fileName = info.Name;
                string shortName = Path.GetFileNameWithoutExtension(item);
                string directory = info.Directory.ToString();
                string extension = info.Extension;
                string sourcePath = info.DirectoryName;

                //if file is image
                //if (extension == ".jpg" || extension == ".jpeg")
                if ((String.Compare(extension, ".jpg", true) == 0) || (String.Compare(extension, ".jpeg", true) == 0))
                {
                    try
                    { 
                        DateTime photoDate = GetDateTakenFromImage(item);

                        if (!photoDate.Equals(DateTime.MinValue))
                        {
                            string yearMonth = photoDate.ToString("yyyy-MM");
                            string targetPath = System.IO.Path.Combine(tbTarget.Text, yearMonth);
                            //string manualSortPath = System.IO.Path.Combine(targetPath, "ManualSort");

                            // Use Path class to manipulate file and directory paths.
                            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                            string destFile = System.IO.Path.Combine(targetPath, fileName);

                            //Prepare a destination directory
                            System.IO.Directory.CreateDirectory(targetPath);

                            // To copy a file to another location and 
                            // overwrite the destination file if it already exists.
                            //System.IO.File.Copy(sourceFile, destFile, true);

                            // if the file does not exist in the target directory create it
                            if (!File.Exists(destFile))
                            {
                                // To move a file or folder to a new location:
                                System.IO.File.Copy(sourceFile, destFile);
                            }

                            // else rename the file and put it in the same directory
                            else
                            {
                                dupCounter = dupCounter + 1;
                                fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                                destFile = System.IO.Path.Combine(targetPath, fileName);
                                System.IO.File.Copy(sourceFile, destFile);
                            }
                        }
                        else
                        {
                            string[] folders = directory.Split(Path.DirectorySeparatorChar);
                            string parent = folders[folders.Length - 1];
                            Regex regex = new Regex(@"^[0-9]{4}-[0-9]{2}");
                            Match match = regex.Match(parent);
                            // Match match = Regex.Match(InputStr, Pattern, RegexOptions)
                            if (match.Success)
                            {
                                string yearMonth = match.ToString();
                                string targetPath = System.IO.Path.Combine(tbTarget.Text, yearMonth);
                                //MessageBox.Show("Hooray");
                                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                                string destFile = System.IO.Path.Combine(targetPath, fileName);

                                //Prepare a destination directory
                                System.IO.Directory.CreateDirectory(targetPath);

                                // To copy a file to another location and 
                                // overwrite the destination file if it already exists.
                                //System.IO.File.Copy(sourceFile, destFile, true);

                                // if the file does not exist in the target directory create it
                                if (!File.Exists(destFile))
                                {
                                    // To move a file or folder to a new location:
                                    System.IO.File.Copy(sourceFile, destFile);
                                }

                                // else rename the file and put it in the same directory
                                else
                                {
                                    dupCounter = dupCounter + 1;
                                    fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                                    destFile = System.IO.Path.Combine(targetPath, fileName);
                                    System.IO.File.Copy(sourceFile, destFile);
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {

                        //FileInfo info = new FileInfo(item);
                        //string fullName = item.ToString();
                        //string fileName = info.Name;
                        //string sourcePath = info.DirectoryName;
                        string manualSortPath = System.IO.Path.Combine(tbTarget.Text, "ManualSort");

                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(manualSortPath, fileName);

                        //Prepare a destination directory
                        System.IO.Directory.CreateDirectory(manualSortPath);

                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        //System.IO.File.Copy(sourceFile, destFile, true);

                        // if the file does not exist in the target directory create it
                        if (!File.Exists(destFile))
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile, destFile);
                        }

                        // else rename the file and put it in the same directory
                        else
                        {
                            dupCounter = dupCounter + 1;
                            fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                            destFile = System.IO.Path.Combine(manualSortPath, fileName);
                            System.IO.File.Copy(sourceFile, destFile);
                        }
                    }
                }

                //if file is video
                //if (extension == ".MOV" || extension == ".mp4")
                else if ((String.Compare(extension, ".mov", true) == 0) || (String.Compare(extension, ".mp4", true) == 0))
                {
                    try
                    {
                        string lastAccessTime = info.LastAccessTime.ToString();
                        string creationTime = info.CreationTime.ToString();
                        string lastWriteTime = info.LastWriteTime.ToString("yyyy-MM");

                        string yearMonth = lastWriteTime;
                        string targetPath = System.IO.Path.Combine(tbTarget.Text, "Videos", yearMonth);
                        string manualSortPath = System.IO.Path.Combine(targetPath, "ManualSort");

                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(targetPath, fileName);

                        //Prepare a destination directory
                        System.IO.Directory.CreateDirectory(targetPath);

                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        //System.IO.File.Copy(sourceFile, destFile, true);

                        // if the file does not exist in the target directory create it
                        if (!File.Exists(destFile))
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile, destFile);
                        }

                        // else rename the file and put it in the same directory
                        else
                        {
                            dupCounter = dupCounter + 1;
                            fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                            destFile = System.IO.Path.Combine(targetPath, fileName);
                            System.IO.File.Copy(sourceFile, destFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        //FileInfo info = new FileInfo(item);
                        //string fullName = item.ToString();
                        //string fileName = info.Name;
                        //string sourcePath = info.DirectoryName;
                        string manualSortPath = System.IO.Path.Combine(tbTarget.Text, @"Videos\ManualSort");

                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                        string destFile = System.IO.Path.Combine(manualSortPath, fileName);

                        //Prepare a destination directory
                        System.IO.Directory.CreateDirectory(manualSortPath);

                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        //System.IO.File.Copy(sourceFile, destFile, true);

                        // if the file does not exist in the target directory create it
                        if (!File.Exists(destFile))
                        {
                            // To move a file or folder to a new location:
                            System.IO.File.Copy(sourceFile, destFile);
                        }

                        // else rename the file and put it in the same directory
                        else
                        {
                            dupCounter = dupCounter + 1;
                            fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                            destFile = System.IO.Path.Combine(manualSortPath, fileName);
                            System.IO.File.Copy(sourceFile, destFile);
                        }
                    }
                }
                else
                {
                    string[] folders = directory.Split(Path.DirectorySeparatorChar);
                    string parent = folders[folders.Length - 1];

                    string manualSortPath = System.IO.Path.Combine(tbTarget.Text, @"\ManualSort", parent);

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                    string destFile = System.IO.Path.Combine(manualSortPath, fileName);

                    //Prepare a destination directory
                    System.IO.Directory.CreateDirectory(manualSortPath);

                    // To copy a file to another location and 
                    // overwrite the destination file if it already exists.
                    //System.IO.File.Copy(sourceFile, destFile, true);

                    // if the file does not exist in the target directory create it
                    if (!File.Exists(destFile))
                    {
                        // To move a file or folder to a new location:
                        System.IO.File.Copy(sourceFile, destFile);
                    }

                    // else rename the file and put it in the same directory
                    else
                    {
                        dupCounter = dupCounter + 1;
                        fileName = string.Format("{0}_{1}{2}", shortName, dupCounter.ToString(), extension);
                        destFile = System.IO.Path.Combine(manualSortPath, fileName);
                        System.IO.File.Copy(sourceFile, destFile);
                    }
                }
            }
            MessageBox.Show("Finished.");
        }

        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue; 
                //throw;
            }

        }
    }
}
