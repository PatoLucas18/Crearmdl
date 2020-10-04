Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1
    


    Dim zliblong As Integer
    Dim unzliblong As Integer
    Dim zlibfile() As Byte
    Dim hexString As String
    Dim vOut As Byte()

    Class Zlibtool
        <DllImport("zlib1.dll", EntryPoint:="compress", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.Cdecl)> _
        Friend Shared Function CompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        End Function

        <DllImport("zlib1.dll", EntryPoint:="uncompress", CallingConvention:=CallingConvention.Cdecl)> _
        Friend Shared Function UncompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        End Function
    End Class

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConvert.Click
        If TB3ds.TextLength > 2 Then
        Else
            MsgBox("Select Files", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If TBmdl.TextLength > 2 Then
        Else
            MsgBox("Select Files", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim btemp() As Byte = My.Resources.temp
        System.IO.File.WriteAllBytes("unzlib", btemp)

        Dim fsID As New FileStream(TB3ds.Text, FileMode.Open)
        Dim brID As New BinaryReader(fsID)

        fsID.Position = 0
        Dim Id3dfile As Integer = brID.ReadInt16()


        fsID.Close()
        brID.Close()
        Select Case Id3dfile
            Case CInt("&H4D4D")
                import3ds(True)
            Case CInt("&H6C70")
                importply(True)
            Case Else
                MsgBox("Modelo 3d no Compatible, use formato 3ds o ply")
                My.Computer.FileSystem.DeleteFile("unzlib")
                Exit Sub
        End Select



        'Leer un texto de un archivo bin 656147
        Dim unzlibfs2 As New FileStream("unzlib", FileMode.Open)
        Dim unzlibbr2 As New BinaryReader(unzlibfs2)


        Dim unzlibfile2() As Byte
        unzlibfs2.Seek(0, SeekOrigin.Begin)
        unzlibfile2 = unzlibbr2.ReadBytes(unzlibfs2.Length)
        unzliblong = unzlibfs2.Length
        Dim zlibfilenew(unzlibfs2.Length) As Byte
        Zlibtool.CompressByteArray(zlibfilenew, unzlibfs2.Length, unzlibfile2, unzlibfs2.Length)
        'System.IO.File.WriteAllBytes(archivo & "_2", bytes)

        Dim b As Byte() = {&H0, &H0, &H1, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
        System.IO.File.WriteAllBytes(TBmdl.Text, b)

        Dim zlibfs2 As New FileStream(TBmdl.Text, FileMode.Open)
        Dim zlibbw2 As New BinaryWriter(zlibfs2)




        'Buscar bytes seguidos en cero
        Dim find As Byte = 0
        Dim index0 As Integer = 0
        Dim index2 As Integer = 0
        Dim index3 As Integer = 0
        Dim index4 As Integer = 0

        Do
            index0 = System.Array.IndexOf(Of Byte)(zlibfilenew, find, index0 + 1)
            index2 = System.Array.IndexOf(Of Byte)(zlibfilenew, find, index0 + 2)
            index3 = System.Array.IndexOf(Of Byte)(zlibfilenew, find, index2 + 3)
            index4 = System.Array.IndexOf(Of Byte)(zlibfilenew, find, index3 + 4)
            If index4 - index0 = 9 Then
                Exit Do
            End If
        Loop


        zlibfs2.Position = 32
        zlibfs2.Write(zlibfilenew, 0, index0 + 16)
        zlibfs2.Position = 33
        zlibbw2.Write(Byte.Parse(CInt("&HDA")))
        zlibfs2.Position = 4
        zlibbw2.Write(Int32.Parse(index0))
        zlibfs2.Position = 8
        zlibbw2.Write(Int32.Parse(unzliblong))
        zlibfs2.Close()
        zlibbw2.Close()


        unzlibfs2.Close()
        unzlibbr2.Close()
        MsgBox("OK", MsgBoxStyle.Information)
        Try

            My.Computer.FileSystem.DeleteFile("unzlib")
        Catch ex As Exception

        End Try
    End Sub

   

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.OFD3ds.Title = "Select file 3ds"
        '' abrir el diálogo 
        If OFD3ds.ShowDialog = Windows.Forms.DialogResult.OK Then
            TB3ds.Text = OFD3ds.FileName
            My.Settings.patch1 = OFD3ds.FileName
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.SFDmdl.Title = "Save File"
        '' abrir el diálogo 
        If SFDmdl.ShowDialog = Windows.Forms.DialogResult.OK Then
            TBmdl.Text = SFDmdl.FileName
            My.Settings.patch2 = SFDmdl.FileName
            My.Settings.Save()
        End If
    End Sub

    Private Sub import3ds(ByVal import3ds As Boolean)
        Dim fs As New FileStream(TB3ds.Text, FileMode.Open)
        Dim br As New BinaryReader(fs)

        Dim fsOut As New FileStream("unzlib", FileMode.OpenOrCreate)
        Dim bwOut As New BinaryWriter(fsOut)


        fs.Seek(0, SeekOrigin.Begin)
        Dim fsfile3ds() As Byte = br.ReadBytes(fs.Length)


        'Buscar Vertices
        Dim find_Vertices As Byte = CInt("&H41")
        Dim index As Integer = 0
        Dim numeros_vertices As Integer = 0
        Dim numerotriangulos As Integer = 0

        Do

            If index >= fsfile3ds.Length Then Exit Do
            If index = -1 Then Exit Do
            index = System.Array.IndexOf(Of Byte)(fsfile3ds, find_Vertices, index + 1)
            If index >= fsfile3ds.Length Then Exit Do
            If index = -1 Then Exit Do

            fs.Position = index - 1
            Dim offset As Integer = br.ReadInt16()
            Select Case offset
                Case CInt("&H4110") 'Vertices
                    fs.Position = index + 5
                    numeros_vertices = br.ReadInt16()
                    
                    'Arranque Offset de vertice en el 3ds
                    Dim offsetout As Integer = index + 7
                    'Arranque Offset de vertice en el mdl
                    Dim offsetout2 As Integer = 160

                    fs.Position = index + 1
                    index += br.ReadInt32() - 1

                    'Dim tamano_Diferenciax As Single = 3.3 / BitConverter.ToSingle(fsfile3ds, offsetout)

                    'Pasar vertices del 3ds al mdl
                    For i = 1 To numeros_vertices


                        fsOut.Position = offsetout2
                        fsOut.Write(fsfile3ds, offsetout, 12)

                        'colocar Normal x,y,z, divido el vertice (float) en 7
                        vOut = BitConverter.GetBytes(BitConverter.ToSingle(fsfile3ds, offsetout) / 7)

                        fsOut.Position = offsetout2 + 12
                        fsOut.Write(vOut, 0, 4)

                        vOut = BitConverter.GetBytes(BitConverter.ToSingle(fsfile3ds, offsetout + 4) / 7)

                        fsOut.Position = offsetout2 + 16
                        fsOut.Write(vOut, 0, 4)

                        vOut = BitConverter.GetBytes(BitConverter.ToSingle(fsfile3ds, offsetout + 8) / 7)

                        fsOut.Position = offsetout2 + 20
                        fsOut.Write(vOut, 0, 4)

                        'Sumar offsets
                        offsetout += 12
                        offsetout2 += 32
                    Next
                    

                Case CInt("&H4120")  'Triangulos
                    fs.Position = index + 5
                    numerotriangulos = br.ReadInt16()

                    Dim offsetout As Integer = index + 7
                    Dim offsetout2 As Integer = (numeros_vertices * 32) + 160 + 4

                    fs.Position = index + 1
                    index += numerotriangulos * 8

                    For i = 1 To numerotriangulos
                        fsOut.Position = offsetout2
                        fsOut.Write(fsfile3ds, offsetout, 6)
                        offsetout += 8
                        offsetout2 += 6
                    Next
                    'Do
                    '    If offsetout >= index Then Exit Do
                    '    fsOut.Position = offsetout2
                    '    fsOut.Write(fsfile3ds, offsetout, 6)
                    '    offsetout += 8
                    '    offsetout2 += 6

                    'Loop
                Case CInt("&H4140")  'UV
                    fs.Position = index + 5

                    Dim offsetout As Integer = index + 7
                    Dim offsetout2 As Integer = 184

                    fs.Position = index + 1
                    index += br.ReadInt32() - 1

                    'fsOut.Position = offsetout2
                    'fsOut.Write(fsfile3ds, offsetout, 12)
                    For i = 1 To numeros_vertices
                        'UV X, Y
                        'fsOut.Position = offsetout2
                        'fsOut.Write(fsfile3ds, offsetout, 8)

                        'UV X
                        fsOut.Position = offsetout2
                        fsOut.Write(fsfile3ds, offsetout, 4)


                        'UV Y 'invertir!
                        vOut = BitConverter.GetBytes(1 - BitConverter.ToSingle(fsfile3ds, offsetout + 4))

                        fsOut.Position = offsetout2 + 4
                        fsOut.Write(vOut, 0, 4)

                        offsetout += 8
                        offsetout2 += 32
                    Next

            End Select

        Loop
        fsOut.Close()
        bwOut.Close()
        fs.Close()
        br.Close()

        'Dim fsfinal As New FileStream("unzlib", FileMode.Open)
        'Dim brfinal As New BinaryReader(fsfinal)

        'Dim index1 As Integer = fsfinal.Length
        ''MsgBox(index1)

        'fsfinal.Close()
        'brfinal.Close()

        Dim fs3 As New FileStream("unzlib", FileMode.OpenOrCreate)
        Dim bw As New BinaryWriter(fs3)
        Dim index1 As Integer = fs3.Length
        fs3.Position = 20
        bw.Write(Int32.Parse((numeros_vertices * 32) + 160))

        fs3.Position = (numeros_vertices * 32) + 160
        bw.Write(Int32.Parse(numerotriangulos * 3 + 1))
        fs3.Position = 100
        bw.Write(Int16.Parse(numerotriangulos * 3 + 1))
        fs3.Position = 94
        bw.Write(Int16.Parse(numerotriangulos * 3 + 1))

        fs3.Position = 28
        bw.Write(Int32.Parse(index1 + 6))
        fs3.Position = 32
        bw.Write(Int32.Parse(index1 + 10))
        fs3.Position = 36
        bw.Write(Int32.Parse(index1 + 34))

        fs3.Position = 98
        bw.Write(Int16.Parse(numeros_vertices))
        fs3.Position = 152
        bw.Write(Int16.Parse(numeros_vertices))

        fs3.Position = index1 + 6
        bw.Write(Int32.Parse(2))
        fs3.Position = index1 + 34
        bw.Write(Int32.Parse(1))

        fs3.Close()
        bw.Close()
        'MsgBox("3ds Imported", MsgBoxStyle.Information)
    End Sub

    Private Sub importply(ByVal importply As Boolean)
        'Leer la configuracion del ini y asignar los indices de los combobox y estados en los check!
        Using fielRead As New StreamReader(TB3ds.Text)
            Dim line As String = fielRead.ReadLine
            Dim offset As Integer = 160
            Dim offsettringles As Integer = 0
            Dim VerticesCount = 0
            Dim FacesCount = 0

            Do While (Not line Is Nothing)

                Dim partes As String() = line.Split(" "c) ' se establece el separador 
                If partes(0) = "element" Then
                    If partes(1) = "vertex" Then
                        VerticesCount = partes(2)
                    End If
                    If partes(1) = "face" Then
                        FacesCount = partes(2)
                        offsettringles = 2 + 160 + VerticesCount * 32
                        Dim fs As New FileStream("unzlib", FileMode.OpenOrCreate)
                        Dim bw As New BinaryWriter(fs)
                        fs.Position = 20
                        bw.Write(Int32.Parse(offsettringles - 2))
                        fs.Position = 98
                        bw.Write(Int16.Parse(VerticesCount))
                        fs.Position = 152
                        bw.Write(Int16.Parse(VerticesCount))

                        fs.Position = offsettringles - 2
                        bw.Write(Int16.Parse(FacesCount * 3))

                        fs.Position = 100
                        bw.Write(Int16.Parse(FacesCount * 3))
                        fs.Position = 94
                        bw.Write(Int16.Parse(FacesCount * 3))
                        Dim final As Integer
                        final = FacesCount * 6 + 2 + 160 + VerticesCount * 32 + 2
                        fs.Position = 28
                        bw.Write(Int32.Parse(final))
                        fs.Position = 32
                        bw.Write(Int32.Parse(final + 4))
                        fs.Position = 36
                        bw.Write(Int32.Parse(final + 32))

                        fs.Position = final + 4
                        bw.Write(Int32.Parse(2))
                        fs.Position = final + 32
                        bw.Write(Int32.Parse(1))
                        fs.Close()
                        bw.Close()
                    End If
                End If


                If IsNumeric(partes(0)) Then
                    If offset < 160 + VerticesCount * 32 Then
                        Dim fs As New FileStream("unzlib", FileMode.OpenOrCreate)
                        Dim bw As New BinaryWriter(fs)



                        Dim floatx As Single = partes(0).Replace(".", ",")

                        Dim bytesx = BitConverter.GetBytes(floatx)
                        fs.Position = offset
                        bw.Write(bytesx, 0, 4)
                        Dim bytesnx = BitConverter.GetBytes(floatx / 7)
                        fs.Position = offset + 12
                        bw.Write(bytesnx, 0, 4)

                        Dim floaty As Single = partes(1).Replace(".", ",")
                        Dim bytesy = BitConverter.GetBytes(floaty)
                        fs.Position = offset + 4
                        bw.Write(bytesy, 0, 4)
                        Dim bytesny = BitConverter.GetBytes(floaty / 7)
                        fs.Position = offset + 16
                        bw.Write(bytesny, 0, 4)

                        Dim floatz As Single = partes(2).Replace(".", ",")
                        Dim bytesz = BitConverter.GetBytes(floatz)
                        fs.Position = offset + 8
                        bw.Write(bytesz, 0, 4)
                        Dim bytesnz = BitConverter.GetBytes(floatz / 7)
                        fs.Position = offset + 20
                        bw.Write(bytesnz, 0, 4)

                        'uv
                        Dim floats As Single = partes(3).Replace(".", ",")
                        Dim bytess = BitConverter.GetBytes(floats)

                        fs.Position = offset + 24
                        bw.Write(bytess, 0, 4)

                        Dim floatt As Single = partes(4).Replace(".", ",")
                        Dim bytest = BitConverter.GetBytes(1 - floatt)

                        fs.Position = offset + 28
                        bw.Write(bytest, 0, 4)
                        fs.Close()
                        bw.Close()
                        offset += 32

                    Else
                        'triangles
                        Dim fs As New FileStream("unzlib", FileMode.OpenOrCreate)
                        Dim bw As New BinaryWriter(fs)

                        If partes(0) = 3 Then
                            fs.Position = offsettringles
                            bw.Write(Int16.Parse(partes(1)))
                            fs.Position = offsettringles + 2
                            bw.Write(Int16.Parse(partes(2)))
                            fs.Position = offsettringles + 4
                            bw.Write(Int16.Parse(partes(3)))
                            offsettringles += 6
                        Else

                        End If

                        fs.Close()
                        bw.Close()
                    End If
                End If

                line = fielRead.ReadLine
                If line Is Nothing Then Exit Do


            Loop
            'MsgBox("ply Imported", MsgBoxStyle.Information)
        End Using
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Cargar configuraciones
        TB3ds.Text = My.Settings.patch1
        TBmdl.Text = My.Settings.patch2
        Me.Width = My.Settings.size
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Guardar tamaño de ancho del formulario
        My.Settings.size = Me.Width
        My.Settings.Save()
    End Sub
End Class
