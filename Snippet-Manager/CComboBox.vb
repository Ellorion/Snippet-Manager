Public Class CComboBox
    Inherits ComboBox

    ' Ver. 1.0.3

    Private Enum OperationType
        None
        Add
        Rename
        Remove
    End Enum

    Public Event EntryAdding(sender As Object, entry As String)
    Public Event EntryRenaming(sender As Object, renamedEntry As String, entry As String)
    Public Event EntryRemoving(sender As Object, entry As String)
    Public Event DataLoad(sender As Object, ByRef items() As Object)
    Public Event DataLoaded(sender As Object)

    Private strOldEntry As String = ""
    Private strNewEntry As String = ""
    Private otCombo As OperationType = OperationType.None
    Private bReloading As Boolean = False

    Public Sub LoadData()
        If bReloading Then Return
        bReloading = True

        Dim myItems() As Object = Nothing

        RaiseEvent DataLoad(Me, myItems)

        Me.Items.Clear()
        If Not myItems Is Nothing Then
            Me.Items.AddRange(myItems)
        End If

        Me.ForeColor = Control.DefaultForeColor

        Dim index As Integer = Me.FindStringExact(strNewEntry)
        If index >= 0 Then
            Me.SelectedIndex = index
        Else
            Me.Text = ""
        End If

        'Me.Text = strNewEntry
        Me.SelectionStart = Me.Text.Length
        Me.SelectionLength = Me.SelectionStart
        otCombo = OperationType.None

        RaiseEvent DataLoaded(Me)

        bReloading = False
    End Sub

    ''' <summary>
    ''' Returns text content (not including the operation message)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public ReadOnly Property TextReal As String
    '    Get
    '        Return strNewEntry
    '    End Get
    'End Property

    Public Overrides Property Text As String
        Get
            If otCombo = OperationType.None Then
                Return MyBase.Text
            End If

            Return strNewEntry
        End Get

        Set(value As String)
            MyBase.Text = value
        End Set
    End Property

    Private Sub CComboBox_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Insert Or e.KeyCode = Keys.Delete Then
            If otCombo <> OperationType.None Then Return

            Dim msg As String = ""
            Dim myColor As Color = Color.Black

            If e.KeyCode = Keys.Insert Then
                If strNewEntry.Length = 0 Then Return

                If strOldEntry.Length <> 0 Then
                    If Me.Items.Contains(strNewEntry) Then Return

                    msg = "Rename: """ + strOldEntry + """ -> """ + strNewEntry + """?"
                    myColor = Color.DarkBlue
                    otCombo = OperationType.Rename
                Else
                    If Me.Items.Contains(strNewEntry) Then Return

                    msg = "Insert: """ + strNewEntry + """?"
                    myColor = Color.DarkGreen
                    otCombo = OperationType.Add
                End If
            End If

            If e.KeyCode = Keys.Delete Then
                If Not Me.Items.Contains(strNewEntry) Then Return

                msg = "Delete: """ + strNewEntry + """?"
                myColor = Color.DarkRed
                otCombo = OperationType.Remove
            End If

            Me.ForeColor = myColor
            Me.Text = msg
            Me.SelectionStart = msg.Length
            Me.SelectionLength = Me.SelectionStart

            Return
        End If

        If otCombo <> OperationType.None Then
            If e.KeyCode = Keys.Escape Then
                LoadData()

            ElseIf e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
                Select Case otCombo
                    Case OperationType.Add
                        RaiseEvent EntryAdding(Me, strNewEntry)
                    Case OperationType.Rename
                        RaiseEvent EntryRenaming(Me, strNewEntry, strOldEntry)
                    Case OperationType.Remove
                        RaiseEvent EntryRemoving(Me, strNewEntry)
                        strNewEntry = ""
                End Select

                LoadData()
            End If

            Return
        End If

        strNewEntry = Me.Text.Trim
    End Sub

    Private Sub CComboBox_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        LoadData()
    End Sub

    Private Sub CComboBox_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Me.SelectedIndexChanged
        strOldEntry = Me.Text.Trim
        strNewEntry = strOldEntry
    End Sub

    Private Sub CComboBox_TextChanged(sender As Object, e As System.EventArgs) Handles Me.TextChanged
        If Me.Text.Length = 0 Then strOldEntry = ""
    End Sub
End Class
