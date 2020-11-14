Imports System.IO

Public Class Form1

    Private Enum VersionFileType
        ApplicationProject
        SetupProject
        InstallerScript
    End Enum


    Private Const TOP_PROJECT_FOLDER As String = "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail\"
    Private Const PATH_TO_VISUAL_STUDIO As String = """C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe""" '-- 12=VS2013;  14=VS2015
    Private Const PATH_TO_OBFUSCATOR As String = """C:\Users\John\Documents\Visual Studio 2008\Projects\XenoCode 2006\XBuild.exe"""
    Private Const PATH_TO_PORTABLE_INSTALLER As String = TOP_PROJECT_FOLDER & "Builders\TrulyMailPortableInstallerNSIS7\TrulyMailPortableSetup.exe"
    Private Const PATH_TO_PORTABLE_INSTALLER_README As String = TOP_PROJECT_FOLDER & "Builders\TrulyMailPortableInstallerNSIS7\read-me.rtf"

    Private Const PATH_TO_PORTABLE_SCRIPT As String = """" & TOP_PROJECT_FOLDER & "Builders\TrulyMailPortableInstallerNSIS7\TrulyMailPortableInstaller.nsi"""
    Private Const PATH_TO_PORTABLE_SCRIPT_ENGINE As String = """C:\Program Files (x86)\NSIS\makensisw.exe"""
    Private Const PATH_TO_VERSION_SPECIFIC_INSTALLERS As String = TOP_PROJECT_FOLDER & "Builders\TrulyMailPortableInstallerNSIS7\"
    Private WithEvents m_zip As Ionic.Zip.ZipFile


    Private m_boolCancel As Boolean = False
    

    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Try
            btnRun.Enabled = False
            btnCancel.Enabled = True
            m_boolCancel = False

            txtVersion.Enabled = False
            Me.txtVersion.Text = Me.txtVersion.Text.Trim()

            Me.ProgressBar2.Maximum = 6 '-- Total steps
            Me.ProgressBar2.Value = 0
            ProgressBar2.Visible = True
            lblOverallProgress.Visible = True

            Me.Refresh()

            If ValidateForm() Then
                Do
                    If m_boolCancel Then Exit Do

                    '-- Prepare version-specific folder
                    Dim strVersionSpecificFolder As String = PATH_TO_VERSION_SPECIFIC_INSTALLERS & txtVersion.Text.Trim
                    If System.IO.Directory.Exists(strVersionSpecificFolder) Then
                        AddOutput("Deleting any existing files in version-specific folder: " & strVersionSpecificFolder)
                        Dim strFiles() As String = Directory.GetFiles(strVersionSpecificFolder, "*.*", SearchOption.AllDirectories)
                        For Each strFile As String In strFiles
                            Try
                                System.IO.File.Delete(strFile)
                            Catch ex As Exception
                                AddOutput("--- ERROR: Could not delete existing file: " & strFile)
                                AddOutput("Reason: " & ex.Message)
                            End Try
                        Next
                    Else
                        AddOutput("Creating version-specific folder: " & strVersionSpecificFolder)
                        System.IO.Directory.CreateDirectory(strVersionSpecificFolder)
                    End If
                    IncrementOverallProgress()


                    '-- Backup source
                    If m_boolCancel Then Exit Do
                    If chkBackupSource.Checked Then
                        AddOutput("Backing up source")
                        BackupSource()
                    End If
                    ResetProgress()

                    '-- First we setup version info, stuff that affects standard and portable both
                    If chkBuildPortable.Checked Then
                        '-- set version
                        AddOutput("Setting new version: " & txtVersion.Text)
                        AddOutput("Previous version: " & SetCurrentVersion(TOP_PROJECT_FOLDER & "TrulyMailClient7\My Project\AssemblyInfo.vb", txtVersion.Text, VersionFileType.ApplicationProject))
                        If m_boolCancel Then Exit Do
                    End If


                    '============================================
                    '====  P O R T A B L E   V E R S I O N   ====
                    '============================================

                    '-- Here we build the portable version only 
                    If chkBuildPortable.Checked Then
                        If m_boolCancel Then Exit Do

                        '-- build exe

                        '-- set for standard
                        AddOutput("Converting application (to portable version)")
                        ConvertEXE(True)

                        AddOutput("Building application (portable version)")
                        BuildEXE()
                    End If
                    IncrementOverallProgress()


                    '-- Obfuscate
                    If m_boolCancel Then Exit Do
                    If chkBuildPortable.Checked Then
                        '-- obfuscate the exe and support files
                        AddOutput("Obfuscating applications")
                        ObfuscateEXEs()
                    End If
                    IncrementOverallProgress()




                    '-- Portable installer
                    If chkBuildPortable.Checked Then
                        AddOutput("Building portable installer")
                        BuildPortableInstaller(strVersionSpecificFolder)
                    End If
                    If m_boolCancel Then Exit Do
                    IncrementOverallProgress()


                    IncrementOverallProgress()

            If m_boolCancel Then Exit Do

            Exit Do '-- not really a loop, just a way to jump out at various points
                Loop
            End If

            If m_boolCancel Then
                AddOutput("Cancelled")
            Else
                AddOutput("Done")
            End If


            btnRun.Enabled = True
            btnCancel.Enabled = False
            txtVersion.Enabled = True
        Catch ex As Exception
            AddOutput("Unrecoverable error: " & ex.Message)
        End Try

    End Sub
    Private Function QualifyPath(ByVal path As String) As String
        If path.EndsWith("\") Then
            Return path
        Else
            Return path & "\"
        End If
    End Function
    Private Sub AddOutput(ByVal newText As String)
        txtOutput.Text = Date.Now.ToString("HH:mm:ss") & "   " & newText & Environment.NewLine & txtOutput.Text
        Application.DoEvents()
    End Sub
    Private Function ValidateForm() As Boolean
        '-- add any form validation logic here, including messageboxes to inform of errors
        Return True
    End Function
    Private Sub ResetProgress()
        Me.ProgressBar2.Value = 2
        Application.DoEvents() '-- me.refresh is not repainting the entire form
    End Sub
    Private Sub IncrementOverallProgress()
        Me.ProgressBar2.Value += 1
        Application.DoEvents() '-- me.refresh is not repainting the entire form
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_boolCancel = True
        btnCancel.Enabled = False
    End Sub
    ''' <summary>
    ''' Sets the new version and returns the original version
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="NewVersion"></param>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCurrentVersion(ByVal FileName As String, ByVal NewVersion As String, ByVal Type As VersionFileType) As String
        Dim strOriginalVersion As String = String.Empty
        Dim strAsmInfo As String = FileName.Replace("""", String.Empty)
        Dim sr As StreamReader = File.OpenText(strAsmInfo)
        Dim strAsmInfoContents As String = String.Empty
        Dim strLine As String
        Dim strProductCode As String

        strProductCode = Guid.NewGuid.ToString().ToUpper()

        '-- Set the version info
        Do Until sr.EndOfStream
            strLine = sr.ReadLine

            '-- Replace the version
            Select Case Type
                Case VersionFileType.ApplicationProject
                    If strLine.StartsWith("<Assembly: AssemblyFileVersion") Then
                        strOriginalVersion = strLine.Substring(32, strLine.IndexOf(")>") - 33)
                        If NewVersion.Length > 0 Then
                            strLine = "<Assembly: AssemblyFileVersion(""" & NewVersion & """)>"
                        End If
                    End If
                Case VersionFileType.SetupProject
                    If strLine.StartsWith("        ""ProductVersion"" = ""8:") Then
                        If NewVersion.Length > 0 Then
                            strLine = "        ""ProductVersion"" = ""8:" & NewVersion & """"
                        End If
                    ElseIf strLine.StartsWith("        ""ProductCode"" = ""8:{") Then
                        If NewVersion.Length > 0 Then
                            strLine = "        ""ProductCode"" = ""8:{" & strProductCode & "}"""
                        End If
                    End If
                Case VersionFileType.InstallerScript
                    If strLine.StartsWith("!define PRODUCT_VERSION ") Then
                        strOriginalVersion = strLine.Substring(25, strLine.Length - 26)
                        If NewVersion.Length > 0 Then
                            strLine = "!define PRODUCT_VERSION """ & NewVersion & """"
                        End If
                    End If
            End Select

            strAsmInfoContents &= strLine & Environment.NewLine
        Loop
        sr.Close()

        If NewVersion.Length > 0 Then
            '-- Now, write the assembly info file back
            Dim tw As System.IO.TextWriter = File.CreateText(strAsmInfo)
            tw.Write(strAsmInfoContents)
            tw.Close()
        End If

        Return strOriginalVersion
    End Function
    Private Sub BuildPortableInstaller(ByVal destinationFolder As String)
        '-- set version in portable installer
        AddOutput("Setting version in portable installer script: " & txtVersion.Text)
        Dim strPreviousVersion As String = SetCurrentVersion(PATH_TO_PORTABLE_SCRIPT, txtVersion.Text, VersionFileType.InstallerScript)
        AddOutput("Previous version was: " & strPreviousVersion)

        If File.Exists(PATH_TO_PORTABLE_INSTALLER) Then
            AddOutput("Deleting existing portable installer")
            File.Delete(PATH_TO_PORTABLE_INSTALLER)
        End If

        Dim proc As System.Diagnostics.Process
        proc = System.Diagnostics.Process.Start(PATH_TO_PORTABLE_SCRIPT_ENGINE, PATH_TO_PORTABLE_SCRIPT)
        proc.WaitForExit() '-- Hold here until build is done

        Try
            '-- portable installer is saved as a zip file for easier downloading
            Using zip As New Ionic.Zip.ZipFile()
                zip.AddFile(PATH_TO_PORTABLE_INSTALLER, String.Empty)
                zip.AddFile(PATH_TO_PORTABLE_INSTALLER_README, String.Empty)
                zip.Save(QualifyPath(destinationFolder) & System.IO.Path.GetFileNameWithoutExtension(PATH_TO_PORTABLE_INSTALLER) & ".zip")
            End Using
        Catch ex As Exception
            AddOutput("Error: " & ex.Message)
            m_boolCancel = True
        End Try
    End Sub

    Private Sub BuildEXE()
        Dim proc As System.Diagnostics.Process
        'proc = System.Diagnostics.Process.Start(PATH_TO_VISUAL_STUDIO, "/build ""Release|Any CPU"" """ & TOP_PROJECT_FOLDER & "TrulyMail.sln""")
        'proc.WaitForExit() '-- Hold here until build is done

        proc = System.Diagnostics.Process.Start(PATH_TO_VISUAL_STUDIO, "/clean ""Release|Any CPU"" """ & TOP_PROJECT_FOLDER & "TrulyMailClient7\TrulyMailClient7.sln""")
        proc.WaitForExit() '-- Hold here until build is done

        proc = System.Diagnostics.Process.Start(PATH_TO_VISUAL_STUDIO, "/rebuild ""Release|Any CPU"" """ & TOP_PROJECT_FOLDER & "TrulyMailClient7\TrulyMailClient7.sln""")
        proc.WaitForExit() '-- Hold here until build is done
    End Sub
    Private Sub ObfuscateEXEs()
        '------------------------------
        '-- First, the TrulyMail.exe
        '------------------------------
        '-- First, copy off everything to the non-obfuscated folder
        AddOutput("Obfuscating TrulyMail.exe")
        MoveToNonObfuscatedMainEXE("tmed.dll")
        MoveToNonObfuscatedMainEXE("TrulyMail.Cryptography.dll")
        MoveToNonObfuscatedMainEXE("TrulyMail.exe")
        MoveToNonObfuscatedMainEXE("Ionic.Zip.dll")
        MoveToNonObfuscatedMainEXE("Argotic.Core.dll")
        MoveToNonObfuscatedMainEXE("Argotic.Extensions.dll")
        MoveToNonObfuscatedMainEXE("Argotic.Common.dll")
        MoveToNonObfuscatedMainEXE("ObjectListView.dll")
        MoveToNonObfuscatedMainEXE("HTMLparserLibDotNet20.dll")

        '-- Now obfuscate
        Dim proc As New System.Diagnostics.Process()
        ''-- Must have quotes around these or they won't work
        proc.StartInfo.FileName = PATH_TO_OBFUSCATOR

        proc.StartInfo.Arguments = """" & TOP_PROJECT_FOLDER & "TrulyMailClient7\bin\Release\TrulyMail.postbuild"""
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        proc.Start()

        proc.WaitForExit() '-- Hold here until build is done


    End Sub
    Private Sub MoveToNonObfuscatedMainEXE(ByVal filename As String)
        Const SOURCE_FOLDER As String = "TrulyMailClient7\bin\Release\"
        Const DESTINATION_FOLDER As String = "TrulyMailClient7\bin\Release\NonObfuscated\"
        Dim strSource As String
        Dim strDestination As String

        If Not System.IO.Directory.Exists(TOP_PROJECT_FOLDER & DESTINATION_FOLDER) Then
            System.IO.Directory.CreateDirectory(TOP_PROJECT_FOLDER & DESTINATION_FOLDER)
        End If

        strSource = TOP_PROJECT_FOLDER & SOURCE_FOLDER & filename
        strDestination = TOP_PROJECT_FOLDER & DESTINATION_FOLDER & filename

        If System.IO.File.Exists(strDestination) Then
            System.IO.File.Delete(strDestination)
        End If
        System.IO.File.Move(strSource, strDestination)
    End Sub
    Private Sub ConvertEXE(ByVal toPortable As Boolean)
        '-- Here we update the .exe.config file so each product has the right settings
        Dim strFilename As String = TOP_PROJECT_FOLDER & "TrulyMailClient7\TrulyMailClient7.vbproj"

        Dim xDoc As New System.Xml.XmlDocument()
        xDoc.Load(strFilename)

        Dim boolDone As Boolean


        '-- Namespaces and linq were too much trouble so I just walk the tree
        For Each xElement As Xml.XmlElement In xDoc.ChildNodes(1).ChildNodes
            If xElement.Name = "PropertyGroup" Then
                If xElement.GetAttribute("Condition") = " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' " Then
                    For Each xElement2 As Xml.XmlElement In xElement.ChildNodes
                        If xElement2.Name = "DefineConstants" Then
                            xElement2.InnerText = "BUILD_AS_PORTABLE=" & toPortable.ToString
                            boolDone = True
                            Exit For
                        End If
                    Next
                    If boolDone Then
                        Exit For
                    End If
                End If
            End If
        Next

        xDoc.Save(strFilename)
    End Sub

    Private Sub ClearFolder(ByVal folder As String)
        Try
            Dim files() As String = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
            For intCounter As Integer = 0 To files.Length - 1
                File.Delete(files(intCounter))
            Next

        Catch ex As Exception
            MessageBox.Show("Error clearing setup folder (" & folder & ")." & Environment.NewLine & Environment.NewLine & _
                ex.ToString())
        End Try

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text & " -- Version: " & Application.ProductVersion
            txtVersion.Text = SetCurrentVersion(TOP_PROJECT_FOLDER & "TrulyMailClient7\My Project\AssemblyInfo.vb", txtVersion.Text, VersionFileType.ApplicationProject)
        Catch ex As Exception
            AddOutput("Could not set current version. " & ex.Message)
        End Try
    End Sub

    Private Sub chkBuildPortable_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chkBuildPortable.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(PATH_TO_PORTABLE_INSTALLER))
        End If
    End Sub

    Private Sub BackupSource()
        m_zip = New Ionic.Zip.ZipFile()

        '-- First, move the text data (or source zip will be 3-4GB)
        Dim strBackupLocation As String = "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail_TEMP_HOLDING_BUILDER\"

        AddOutput("Moving data folders to TrulyMail_TEMP_HOLDING_BUILDER")
        Try
            System.IO.Directory.Move("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail\TrulyMailClient7\bin\Data", "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail_TEMP_HOLDING_BUILDER\Data1")
            System.IO.Directory.Move("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail\TrulyMailClient7\bin\x86\Data", "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail_TEMP_HOLDING_BUILDER\Data2")
            AddOutput("Data folders moved")
        Catch ex As Exception
            AddOutput("Error moving data folders: " & ex.Message)
            AddOutput("Continuing build")
        End Try
        
        '-- Now backup
        m_zip.AddDirectory("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail", "Projects\TrulyMail")

        m_zip.Save("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMailSource" & txtVersion.Text & ".zip")
        m_zip.Dispose()

        '-- Now move the test data back
        Try
            AddOutput("Restoring data folders from TrulyMail_TEMP_HOLDING_BUILDER")
            System.IO.Directory.Move("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail_TEMP_HOLDING_BUILDER\Data1", "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail\TrulyMailClient7\bin\Data")
            System.IO.Directory.Move("C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail_TEMP_HOLDING_BUILDER\Data2", "C:\Users\John\Documents\Visual Studio 2010\Projects\TrulyMail\TrulyMailClient7\bin\x86\Data")
            AddOutput("Data folders restored")
        Catch ex As Exception
            AddOutput("Error restoring data folders: " & ex.Message)
            AddOutput("Continuing build")
        End Try


    End Sub
    Private Sub m_zip_SaveProgress(ByVal sender As Object, ByVal e As Ionic.Zip.SaveProgressEventArgs) Handles m_zip.SaveProgress
        If e.EntriesTotal > 0 Then
            Me.ProgressBar2.Value = Me.ProgressBar2.Maximum * (e.EntriesSaved / e.EntriesTotal)
            e.Cancel = m_boolCancel
            Application.DoEvents()
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblClearOutput.LinkClicked
        txtOutput.Text = String.Empty
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        chkBackupSource.Checked = False
        chkBuildPortable.Checked = True
        btnRun.PerformClick()
    End Sub
End Class
