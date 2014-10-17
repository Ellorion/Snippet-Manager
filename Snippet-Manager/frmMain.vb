Public Class frmMain
    Inherits CForm

    Const EM_SETTABSTOPS As Integer = &HCB
    Declare Function SendMessageA Lib "user32" (ByVal TBHandle As IntPtr, _
                                               ByVal EM_SETTABSTOPS As Integer, _
                                               ByVal wParam As Integer, _
                                               ByRef lParam As Integer) As Boolean

    Private mySQL As New CSQLiteSnippet

    Private sDBFile As String = "SnippetDB.db"

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        mySQL.CreateDatabase(sDBFile)
        mySQL.OpenDatabase(sDBFile)

        cbCategory.LoadData()
        cbSubCategory.LoadData()
        cbSnippet.LoadData()

        SendMessageA(txtData.Handle, EM_SETTABSTOPS, 1, 16)
    End Sub

    Private Sub cbCategory_EntryAdding(sender As Object, entry As String) Handles cbCategory.EntryAdding
        mySQL.SetCategoryName(entry, "")
    End Sub

    Private Sub cbCategory_EntryRemoving(sender As Object, entry As String) Handles cbCategory.EntryRemoving
        mySQL.RemoveCategory(entry)
    End Sub

    Private Sub cbCategory_EntryRenaming(sender As Object, renamedEntry As String, entry As String) Handles cbCategory.EntryRenaming
        mySQL.SetCategoryName(renamedEntry, entry)
    End Sub

    Private Sub cbCategory_Reload(sender As Object, ByRef items() As Object) Handles cbCategory.DataLoad
        items = mySQL.GetCategoryList().ToArray
    End Sub

    Private Sub cbSubCategory_EntryAdding(sender As Object, entry As String) Handles cbSubCategory.EntryAdding
        mySQL.SetCategoryName(entry, "", cbCategory.Text)
    End Sub

    Private Sub cbSubCategory_EntryRemoving(sender As Object, entry As String) Handles cbSubCategory.EntryRemoving
        mySQL.RemoveCategory(entry, cbCategory.Text)
    End Sub

    Private Sub cbSubCategory_EntryRenaming(sender As Object, renamedEntry As String, entry As String) Handles cbSubCategory.EntryRenaming
        mySQL.SetCategoryName(renamedEntry, entry, cbCategory.Text)
    End Sub

    Private Sub cbSubCategory_Reload(sender As Object, ByRef items() As Object) Handles cbSubCategory.DataLoad
        If cbCategory.Text.Length > 0 Then
            items = mySQL.GetCategoryList(cbCategory.Text).ToArray
        End If
    End Sub

    Private Sub cbCategory_Reloaded(sender As Object) Handles cbCategory.DataLoaded
        cbSubCategory.LoadData()
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbCategory.SelectedIndexChanged
        cbSubCategory.LoadData()
    End Sub

    Private Sub cbSnippet_EntryAdding(sender As Object, entry As String) Handles cbSnippet.EntryAdding
        mySQL.AddSnippet(entry, cbSubCategory.Text, cbCategory.Text)
    End Sub

    Private Sub cbSnippet_EntryRemoving(sender As Object, entry As String) Handles cbSnippet.EntryRemoving
        mySQL.RemoveSnippet(entry, cbSubCategory.Text, cbCategory.Text)
    End Sub

    Private Sub cbSnippet_EntryRenaming(sender As Object, renamedEntry As String, entry As String) Handles cbSnippet.EntryRenaming
        mySQL.RenameSnippet(renamedEntry, entry, cbSubCategory.Text, cbCategory.Text)
    End Sub

    Private Sub cbSnippet_Reload(sender As Object, ByRef items() As Object) Handles cbSnippet.DataLoad
        items = mySQL.GetSnippetNames(cbSubCategory.Text, cbCategory.Text).ToArray
    End Sub

    Private Sub cbSubCategory_Reloaded(sender As Object) Handles cbSubCategory.DataLoaded
        cbSnippet.LoadData()
    End Sub

    Private Sub cbSubCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbSubCategory.SelectedIndexChanged
        cbSnippet.LoadData()
    End Sub

    Private Sub cbSnippet_Reloaded(sender As Object) Handles cbSnippet.DataLoaded
        If cbSnippet.Text.Length = 0 Then
            txtData.Text = ""
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        mySQL.SaveSnippet(cbSnippet.Text, cbSubCategory.Text, cbCategory.Text, txtData.Text)
    End Sub

    Private Sub cbSnippet_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbSnippet.SelectedIndexChanged
        txtData.Text = mySQL.GetSnippet(cbSnippet.Text, cbSubCategory.Text, cbCategory.Text)
    End Sub

    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtData.Text)
    End Sub

    Private Sub txtData_GotFocus(sender As Object, e As System.EventArgs) Handles txtData.GotFocus
        txtData.Select(txtData.Text.Length, txtData.Text.Length)
    End Sub
End Class
