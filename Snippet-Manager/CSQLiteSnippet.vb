Public Class CSQLiteSnippet
    Inherits CSQLite

    Public Overrides Function CreateDatabase(sFile As String) As Boolean
        If Not Me.CreateDatabase(sFile, True) Then Return False

        Me.Query("CREATE TABLE IF NOT EXISTS Category ( " +
                 "ID INTEGER NOT NULL," +
                 "Name VARCHAR(255) NOT NULL," +
                 "SubCatID INTEGER NULL," +
                 "PRIMARY KEY(ID)" +
                 ")")

        Me.Query("CREATE TABLE IF NOT EXISTS Snippet (" +
                 "ID INTEGER NOT NULL, " +
                 "Name VARCHAR(255) NOT NULL," +
                 "Data TEXT NOT NULL DEFAULT """"," +
                 "CatID INTEGER NOT NULL" +
                 ")")

        Me.CloseDatabase()
        Return True
    End Function

    Public Function GetCategoryList(Optional parentCategory As String = "") As List(Of String)
        Dim myListQuery As New List(Of List(Of String))
        Dim myList As New List(Of String)

        Dim sql As String = "SELECT Name FROM Category"

        If parentCategory.Length > 0 Then
            sql += " WHERE SubCatID = (SELECT ID FROM Category WHERE Name = """ + parentCategory + """)"
        Else
            sql += " WHERE SubCatID is null"
        End If

        sql += " ORDER BY Name"

        Me.Query(sql, , myListQuery)

        For Each myRow As List(Of String) In myListQuery
            myList.Add(myRow.Item(0).Trim)
        Next

        Return myList
    End Function

    Public Sub SetCategoryName(newName As String, Optional oldName As String = "", Optional parentCategory As String = "")
        If oldName.Trim.Length = 0 Then
            If parentCategory.Length > 0 Then
                Me.Query("INSERT INTO Category(ID, Name, SubCatID) VALUES (" + Me.AutoIncID("Category") + ",'" + newName.Trim + "', (SELECT ID FROM Category WHERE Name = """ + parentCategory + """))")
            Else
                Me.Query("INSERT INTO Category(ID, Name) VALUES (" + Me.AutoIncID("Category") + ",'" + newName.Trim + "')")
            End If

            Return
        End If

        If parentCategory.Length > 0 Then
            Me.Query("UPDATE Category SET Name = '" + newName.Trim + "' WHERE Name = '" + oldName + "' AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + parentCategory + """)")
        Else
            Me.Query("UPDATE Category SET Name = '" + newName.Trim + "' WHERE Name = '" + oldName + "' AND SubCatID is null")
        End If
    End Sub

    Public Sub RemoveCategory(Name As String, Optional parentCategory As String = "")
        Dim sql As String = "DELETE FROM Category WHERE Name = """ + Name + """ AND " +
            "ID NOT IN (SELECT SubCatID FROM Category where SubCatID is not null UNION SELECT CatID FROM Snippet)"

        If parentCategory.Length > 0 Then
            sql += " AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + parentCategory + """)"
        End If

        Me.Query(sql)
    End Sub

    Public Sub RenameSnippet(renamed As String, base As String, category As String, mainCategory As String)
        Me.Query("UPDATE Snippet SET name = """ + renamed + """ WHERE name = """ + base + """ AND CatID = (SELECT ID FROM Category WHERE name = """ + category + """ AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null))")
    End Sub

    Public Sub AddSnippet(name As String, category As String, mainCategory As String)
        Me.Query("INSERT INTO Snippet (ID, Name, CatID) VALUES (" + AutoIncID("Snippet") + ", """ + name + """," +
                 "  (SELECT ID FROM Category WHERE name = """ + category + """ " +
                 "  AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null)" +
                 "  )" +
                 ")")
    End Sub

    Public Sub SaveSnippet(name As String, category As String, mainCategory As String, data As String)
        data = data.Replace("""", """""")

        Me.Query("UPDATE Snippet SET Data = """ + data + """ WHERE name = """ + name + """ AND CatID = (SELECT ID FROM Category WHERE name = """ + category + """ AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null))")
    End Sub

    Public Sub RemoveSnippet(name As String, category As String, mainCategory As String)
        Me.Query("DELETE FROM Snippet WHERE name = """ + name + """ AND CatID = " +
                 "  (SELECT ID FROM Category WHERE name = """ + category + """ " +
                 "  AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null)" +
                 "   )")
    End Sub

    Public Function GetSnippetNames(category As String, mainCategory As String) As List(Of String)
        Dim myListQuery As New List(Of List(Of String))
        Dim myList As New List(Of String)

        Dim sql As String = "SELECT Name FROM Snippet WHERE CatID = " +
                            "   (SELECT ID FROM Category WHERE name = """ + category + """ " +
                            "   AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null)" +
                            "   )"

        Me.Query(sql, , myListQuery)

        For Each myRow As List(Of String) In myListQuery
            myList.Add(myRow.Item(0).Trim)
        Next

        Return myList
    End Function

    Public Function GetSnippet(name As String, category As String, mainCategory As String) As String
        Dim myListQuery As New List(Of List(Of String))
        Dim myList As New List(Of String)

        Dim sql As String = "SELECT Data FROM Snippet WHERE Name = """ + name + """ AND CatID = " +
                            "   (SELECT ID FROM Category WHERE name = """ + category + """ " +
                            "   AND SubCatID = (SELECT ID FROM Category WHERE Name = """ + mainCategory + """ AND SubCatID is null)" +
                            "   )"

        Me.Query(sql, , myListQuery)

        If Not myListQuery Is Nothing Then
            If Not myListQuery.Item(0) Is Nothing Then
                Return myListQuery.Item(0).Item(0)
            End If
        End If

        Return ""
    End Function
End Class
