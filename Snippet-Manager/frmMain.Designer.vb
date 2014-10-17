<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits Snippet_Manager.CForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.cbSnippet = New Snippet_Manager.CComboBox()
        Me.cbSubCategory = New Snippet_Manager.CComboBox()
        Me.cbCategory = New Snippet_Manager.CComboBox()
        Me.txtData = New System.Windows.Forms.TextBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.grpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpMain.Controls.Add(Me.cbSnippet)
        Me.grpMain.Controls.Add(Me.cbSubCategory)
        Me.grpMain.Controls.Add(Me.cbCategory)
        Me.grpMain.Location = New System.Drawing.Point(12, 12)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(584, 78)
        Me.grpMain.TabIndex = 0
        Me.grpMain.TabStop = False
        Me.grpMain.Text = "Category"
        '
        'cbSnippet
        '
        Me.cbSnippet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSnippet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSnippet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSnippet.FormattingEnabled = True
        Me.cbSnippet.Location = New System.Drawing.Point(6, 46)
        Me.cbSnippet.Name = "cbSnippet"
        Me.cbSnippet.Size = New System.Drawing.Size(570, 21)
        Me.cbSnippet.TabIndex = 2
        '
        'cbSubCategory
        '
        Me.cbSubCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSubCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSubCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSubCategory.FormattingEnabled = True
        Me.cbSubCategory.Location = New System.Drawing.Point(214, 19)
        Me.cbSubCategory.Name = "cbSubCategory"
        Me.cbSubCategory.Size = New System.Drawing.Size(362, 21)
        Me.cbSubCategory.TabIndex = 1
        '
        'cbCategory
        '
        Me.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbCategory.FormattingEnabled = True
        Me.cbCategory.Location = New System.Drawing.Point(6, 19)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(202, 21)
        Me.cbCategory.TabIndex = 0
        '
        'txtData
        '
        Me.txtData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtData.Location = New System.Drawing.Point(12, 126)
        Me.txtData.Multiline = True
        Me.txtData.Name = "txtData"
        Me.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtData.Size = New System.Drawing.Size(584, 202)
        Me.txtData.TabIndex = 3
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(12, 96)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(75, 23)
        Me.btnCopy.TabIndex = 4
        Me.btnCopy.TabStop = False
        Me.btnCopy.Text = "&Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(520, 96)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.TabStop = False
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 340)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.txtData)
        Me.Controls.Add(Me.grpMain)
        Me.Name = "frmMain"
        Me.Text = "Snippet-Manager"
        Me.grpMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents cbCategory As Snippet_Manager.CComboBox
    Friend WithEvents cbSubCategory As Snippet_Manager.CComboBox
    Friend WithEvents txtData As System.Windows.Forms.TextBox
    Friend WithEvents cbSnippet As Snippet_Manager.CComboBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button

End Class
