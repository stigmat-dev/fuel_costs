Public Class Form1

    Private Sub CostsBindingNavigator1SaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        Me.CostsBindingSource1.EndEdit()
        Me.TableAdapterManager1.UpdateAll(Me.DatabaseDataSet2)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: данная строка кода позволяет загрузить данные в таблицу "DatabaseDataSet2.costs". При необходимости она может быть перемещена или удалена.
        Me.CostsTableAdapter1.Fill(Me.DatabaseDataSet2.costs)
        'Server = "DESKTOP-NC6F157\SQLEXPRESS"
        'Base = "C:\Users\Стигмат\Desktop\Учет расходов на топливо\DATABASE.MDF"
        Server = "STIGMAT\SQLEXPRESS"
        Base = "C:\Users\Стигмат\Desktop\Fuel\fuel_cost\fuel_cost\DATABASE.MDF"
        Username = "sa"
        Password = "1111"
        GetConnect()


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim data As Date
        Dim data2 As Date
        data = DateTimePicker1.Value.ToString
        data2 = DateTimePicker2.Value.ToString
        LoadGridFromDB(DataGridView1, "select * from costs where date between '" & CDate(data) & "' and '" & CDate(data2) & "'")
    End Sub
    
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DataGridView1.Columns.Clear()
    End Sub

    Dim mRow As Integer = 0
    Dim newpage As Boolean = True
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        With DataGridView1
            Dim fmt As StringFormat = New StringFormat(StringFormatFlags.LineLimit)
            fmt.LineAlignment = StringAlignment.Center
            fmt.Trimming = StringTrimming.EllipsisCharacter
            Dim y As Single = e.MarginBounds.Top
            Do While mRow < .RowCount
                Dim row As DataGridViewRow = .Rows(mRow)
                Dim x As Single = e.MarginBounds.Left
                Dim h As Single = 0
                For Each cell As DataGridViewCell In row.Cells
                    Dim rc As RectangleF = New RectangleF(x, y, cell.Size.Width, cell.Size.Height)
                    e.Graphics.DrawRectangle(Pens.Black, rc.Left, rc.Top, rc.Width, rc.Height)
                    If (newpage) Then
                        e.Graphics.DrawString(DataGridView1.Columns(cell.ColumnIndex).HeaderText, .Font, Brushes.Black, rc, fmt)
                    Else
                        e.Graphics.DrawString(DataGridView1.Rows(cell.RowIndex).Cells(cell.ColumnIndex).FormattedValue.ToString(), .Font, Brushes.Black, rc, fmt)
                    End If
                    x += rc.Width
                    h = Math.Max(h, rc.Height)
                Next
                newpage = False
                y += h
                mRow += 1
                If y + h > e.MarginBounds.Bottom Then
                    e.HasMorePages = True
                    mRow -= 1
                    newpage = True
                    Exit Sub
                End If
            Loop
            mRow = 0
        End With
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PrintPreviewDialog1.ShowDialog()
        PrintPreviewDialog1.Document = PrintDocument1
    End Sub
End Class
