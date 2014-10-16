Public Class frmMain
    Inherits CForm

    Private mySQL As New CSQLiteSnippet

    Private sDBFile As String = "SnippetDB.db"

    Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        mySQL.CreateDatabase(sDBFile)
        mySQL.OpenDatabase(sDBFile)

        cbCategory.ReloadControl()
        cbSubCategory.ReloadControl()
        cbSnippet.ReloadControl()
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

    Private Sub cbCategory_Reload(sender As Object, ByRef items() As Object) Handles cbCategory.Reload
        items = mySQL.GetCategoryList().ToArray
    End Sub

    Private Sub cbSubCategory_EntryAdding(sender As Object, entry As String) Handles cbSubCategory.EntryAdding
        mySQL.SetCategoryName(entry, "", cbCategory.TextReal)
    End Sub

    Private Sub cbSubCategory_EntryRemoving(sender As Object, entry As String) Handles cbSubCategory.EntryRemoving
        mySQL.RemoveCategory(entry, cbCategory.TextReal)
    End Sub

    Private Sub cbSubCategory_EntryRenaming(sender As Object, renamedEntry As String, entry As String) Handles cbSubCategory.EntryRenaming
        mySQL.SetCategoryName(renamedEntry, entry, cbCategory.TextReal)
    End Sub

    Private Sub cbSubCategory_Reload(sender As Object, ByRef items() As Object) Handles cbSubCategory.Reload
        If cbCategory.TextReal.Length > 0 Then
            items = mySQL.GetCategoryList(cbCategory.TextReal).ToArray
        End If
    End Sub

    Private Sub cbCategory_Reloaded(sender As Object) Handles cbCategory.Reloaded
        cbSubCategory.ReloadControl()
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbCategory.SelectedIndexChanged
        cbSubCategory.ReloadControl()
    End Sub

    Private Sub cbSnippet_EntryAdding(sender As Object, entry As String) Handles cbSnippet.EntryAdding
        mySQL.AddSnippet(entry, cbSubCategory.TextReal, cbCategory.TextReal)
    End Sub

    Private Sub cbSnippet_EntryRemoving(sender As Object, entry As String) Handles cbSnippet.EntryRemoving
        mySQL.RemoveSnippet(entry, cbSubCategory.TextReal, cbCategory.TextReal)
    End Sub

    Private Sub cbSnippet_EntryRenaming(sender As Object, renamedEntry As String, entry As String) Handles cbSnippet.EntryRenaming
        mySQL.RenameSnippet(renamedEntry, entry, cbSubCategory.TextReal, cbCategory.TextReal)
    End Sub

    Private Sub cbSnippet_Reload(sender As Object, ByRef items() As Object) Handles cbSnippet.Reload
        items = mySQL.GetSnippetNames(cbSubCategory.TextReal, cbCategory.TextReal).ToArray
    End Sub

    Private Sub cbSubCategory_Reloaded(sender As Object) Handles cbSubCategory.Reloaded
        cbSnippet.ReloadControl()
    End Sub

    Private Sub cbSubCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbSubCategory.SelectedIndexChanged
        cbSnippet.ReloadControl()
    End Sub

    Private Sub cbSnippet_Reloaded(sender As Object) Handles cbSnippet.Reloaded
        If cbSnippet.TextReal.Length = 0 Then
            txtData.Text = ""
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        mySQL.SaveSnippet(cbSnippet.TextReal, cbSubCategory.TextReal, cbCategory.TextReal, txtData.Text)
    End Sub

    Private Sub cbSnippet_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbSnippet.SelectedIndexChanged
        txtData.Text = mySQL.GetSnippet(cbSnippet.TextReal, cbSubCategory.TextReal, cbCategory.TextReal)
    End Sub

    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        Clipboard.SetText(txtData.Text)
    End Sub
End Class
