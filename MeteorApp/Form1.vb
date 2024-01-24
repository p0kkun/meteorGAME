Imports System.Drawing.Text

Public Class Form1
    Dim canvas As New Bitmap(680, 520)
    Dim g As Graphics = Graphics.FromImage(canvas)
    Dim PW, PH As Long  '自機の幅,高さ
    Dim Cpos As Point   'カーソル座標
    Dim enX(20), enY(20) As Long  '隕石の座標
    Dim meteorSpeed As Long = 5  ' 隕石の初期スピード
    Dim meteorcnt As Long = 20
    Dim rand As New Random
    Dim RR As Long              '隕石の半径
    Dim meteorSizes(meteorcnt) As Long ' 隕石の大きさ
    Dim hitFlg As Boolean       'true:当たった
    Dim ecnt, ex, ey As Long    '爆発演出用
    Dim msgcnt As Long         'カウンタ
    Dim titleFlg As Boolean    'true:タイトル表示
    Dim score As Long
    Dim highScore As Long
    Dim myFont As Font = New Font("Arial", 16)

    ' ビーム関連の変数
    Dim beamSpeed As Long = 10  ' ビームのスピード
    Dim beams As New List(Of Beam)()
    ' クラスレベルの変数
    Private isFullScreen As Boolean = False
    Private originalWindowState As FormWindowState
    Private originalFormBorderStyle As FormBorderStyle
    Private originalBounds As Rectangle

    ' フルスクリーンモードの切り替え
    Private Sub ToggleFullScreen()
        If isFullScreen Then
            ' フルスクリーンから通常モードに戻す
            Me.WindowState = originalWindowState
            Me.FormBorderStyle = originalFormBorderStyle
            Me.Bounds = originalBounds
            isFullScreen = False

            ' キャンバスのサイズを元に戻す
            canvas = New Bitmap(680, 520)
        Else
            ' フルスクリーンモードに設定
            originalWindowState = Me.WindowState
            originalFormBorderStyle = Me.FormBorderStyle
            originalBounds = Me.Bounds

            Me.WindowState = FormWindowState.Maximized
            Me.FormBorderStyle = FormBorderStyle.None
            Me.Bounds = Screen.PrimaryScreen.Bounds
            isFullScreen = True

            ' キャンバスのサイズをフルスクリーンサイズに設定
            canvas = New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)
        End If
        If isFullScreen Then
            ' フルスクリーンモードに切り替える際の自機のY座標
            Cpos.Y = Me.ClientSize.Height - PH
        Else
            ' 通常モードに戻す際の自機のY座標
            Cpos.Y = Me.ClientSize.Height - PH
        End If
        ' グラフィックスオブジェクトを更新
        g = Graphics.FromImage(canvas)

        ' pBaseのサイズを調整し、キャンバスをセットして更新
        pBase.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height)
        pBase.Image = canvas
        pBase.Invalidate()  ' pBaseを再描画して更新
    End Sub


    ' 例えば、F11キーでフルスクリーンモードを切り替える
    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.F11 Then
            ToggleFullScreen()
        End If
    End Sub
    ' オブジェクトの位置とサイズを更新するメソッド
    Private Sub UpdateGraphics()
        ' キャンバスサイズに基づいてオブジェクトの位置を計算
        Dim playerX As Integer = Cpos.X / 680 * canvas.Width
        Dim playerY As Integer = Cpos.Y / 520 * canvas.Height
        PW = 41 / 680 * canvas.Width ' 自機の幅をスケーリング
        PH = 51 / 520 * canvas.Height ' 自機の高さをスケーリング
        RR = 35 / 680 * canvas.Width ' 隕石の半径をスケーリング

        ' オブジェクトの描画
        g.DrawImage(pPlayer.Image, New Rectangle(playerX, playerY, PW, PH))
        For i As Long = 0 To meteorcnt
            Dim meteorSize As Long = meteorSizes(i)
            Dim meteorX As Integer = enX(i) / 680 * canvas.Width
            Dim meteorY As Integer = enY(i) / 500 * canvas.Height
            g.DrawImage(pMeteor.Image, New Rectangle(meteorX, meteorY, meteorSize, meteorSize))
        Next

    End Sub


    Public Structure Beam
        Public X As Long
        Public Y As Long
        Public Active As Boolean
    End Structure

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pMeteor.Hide()
        pPlayer.Hide()
        pBG.Hide()
        pExp.Hide()
        pGameover.Hide()
        pMsg.Hide()
        pTitle.Hide()
        pBeam.Hide()
        LoadHighScore()
        DisplayHighScore()
        gameInit()
    End Sub

    '爆発演出
    Private Sub playerExplosion()
        ecnt += 4
        If ecnt > 40 Then
            ecnt = 8
            ex = Cpos.X + rand.Next(40)
            ey = Cpos.Y + rand.Next(50)
        End If
        g.DrawImage(pBG.Image, New Rectangle(0, 0, 680, 520))
        For i As Long = 0 To meteorcnt
            ' 隕石の現在の大きさを取得
            Dim meteorDiameter As Long = meteorSizes(i)

            ' 隕石を描画
            g.DrawImage(pMeteor.Image, New Rectangle(enX(i), enY(i), meteorDiameter, meteorDiameter))
        Next
        g.DrawImage(pPlayer.Image, New Rectangle(Cpos.X, Cpos.Y, PW, PH))
        g.DrawImage(pExp.Image, New Rectangle(ex - ecnt / 2, ey - ecnt / 2, ecnt, ecnt))
        msgcnt += 1
        If msgcnt > 60 Then
            ' ゲームオーバー画像のサイズ
            Dim gameOverWidth As Integer = 350
            Dim gameOverHeight As Integer = 60

            ' ゲームオーバー画像を中央に配置するための座標計算
            Dim gameOverX As Integer = (680 - gameOverWidth) / 2
            Dim gameOverY As Integer = (520 - gameOverHeight) / 2

            g.DrawImage(pGameover.Image, New Rectangle(gameOverX, gameOverY, gameOverWidth, gameOverHeight))

            If (msgcnt Mod 60) > 20 Then
                ' その他のメッセージも中央に配置する場合はこちらも計算
                Dim msgWidth As Integer = 271
                Dim msgHeight As Integer = 26
                Dim msgX As Integer = (680 - msgWidth) / 2
                Dim msgY As Integer = gameOverY + gameOverHeight + 10 ' ゲームオーバー画像の下に少し間隔を空ける

                g.DrawImage(pMsg.Image, New Rectangle(msgX, msgY, msgWidth, msgHeight))
            End If
        End If

        g.DrawString("SCORE:" & score.ToString(), myFont, Brushes.White, 10, 10)
        UpdateHighScore()
        DisplayHighScore()
        pBase.Image = canvas
    End Sub

    Private Sub gameInit()
        PW = 41  '自機の幅
        PH = 51  '自機の高さ
        RR = 70 / 2  '隕石の半径
        meteorSpeed = 5  ' 隕石のスピードを初期値に戻す
        beams.Clear()    ' ビームのリストをクリア
        For i As Long = 0 To meteorcnt
            meteorSizes(i) = rand.Next(40, 150)
            enX(i) = rand.Next(1, 530)  '隕石の初期座標
            enY(i) = rand.Next(1, 900) - 1000
        Next
        hitFlg = False '当たっていない
        ecnt = 40  '爆発演出用
        msgcnt = 0
        titleFlg = True   'True：タイトル表示中
        score = 0
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        UpdateGraphics()
        If titleFlg Then
            titleDisp()
            Exit Sub
        End If
        If hitFlg Then
            playerExplosion()
            Exit Sub
        End If
        g.DrawImage(pBG.Image, New Rectangle(0, 0, 680, 520))

        ' スコアに基づいて隕石のスピードを調整
        If score Mod 50 = 0 Then
            meteorSpeed += 0.5
        End If

        '隕石の移動
        For i As Long = 0 To meteorcnt
            enY(i) += meteorSpeed
            Dim meteorDiameter As Long = meteorSizes(i) ' 隕石の直径
            g.DrawImage(pMeteor.Image, New Rectangle(enX(i), enY(i), meteorDiameter, meteorDiameter))
            If enY(i) > pBase.Height Then   '画面外に出たら上に戻す
                enX(i) = rand.Next(1, 550)
                enY(i) = -rand.Next(100, 300)
            End If
        Next

        If score > 2000 Then
            For i As Long = 0 To meteorcnt
                If meteorSizes(i) < 350 Then
                    meteorSizes(i) = 350
                    enX(i) = rand.Next(1, 350)  '隕石の新しい座標
                    enY(i) = -rand.Next(100, 300)
                End If
            Next
        End If

        Dim beamsToRemove As New List(Of Beam)()  ' 削除するビームの一覧

        ' ビームの更新と描画
        For i As Integer = 0 To beams.Count - 1
            Dim beam As Beam = beams(i)
            beam.Y -= beamSpeed

            beams(i) = beam

            ' ビームの衝突判定
            For j As Integer = 0 To meteorcnt
                ' 隕石の現在の大きさを取得
                Dim meteorDiameter As Long = meteorSizes(j)

                If beam.Y < enY(j) + meteorDiameter AndAlso beam.Y > enY(j) AndAlso beam.X > enX(j) AndAlso beam.X < enX(j) + meteorDiameter Then
                    ' 隕石を画面上部のランダムな位置に再配置
                    enX(j) = rand.Next(1, 650)
                    enY(j) = rand.Next(1, 900) - 1000
                    beamsToRemove.Add(beam)
                    Exit For
                End If
            Next
            ' ビームを描画
            If Not beamsToRemove.Contains(beam) Then
                g.DrawLine(Pens.Yellow, beam.X, beam.Y, beam.X, beam.Y - 10)
            End If
        Next

        For Each b In beamsToRemove
            beams.Remove(b)
        Next

        Cpos = PointToClient(Cursor.Position)
        If Cpos.X < 0 Then
            Cpos.X = 0
        End If
        If Cpos.X > Width - PW Then
            Cpos.X = Width - PW
        End If
        If Cpos.Y < 0 Then
            Cpos.Y = 0
        End If
        If Cpos.Y > Height - PH Then
            Cpos.Y = Height - PH
        End If
        g.DrawImage(pPlayer.Image, New Rectangle(Cpos.X, Cpos.Y, PW, PH))
        score += 1
        g.DrawString("SCORE:" & score.ToString(), myFont, Brushes.White, 10, 10)

        pBase.Image = canvas
        hitCheck()  '当たり判定
    End Sub

    '自機と隕石の当たり判定
    Private Sub hitCheck()
        Dim pcx As Long = Cpos.X + (PW / 2)  '自機の中心座標
        Dim pcy As Long = Cpos.Y + (PH / 2)

        For i As Long = 0 To meteorcnt
            ' 隕石の現在の大きさを取得
            Dim meteorDiameter As Long = meteorSizes(i)

            ' 隕石の中心座標を計算
            Dim ecx As Long = enX(i) + meteorDiameter / 2
            Dim ecy As Long = enY(i) + meteorDiameter / 2

            ' 自機と隕石の中心間の距離を計算
            Dim dis As Long = (ecx - pcx) ^ 2 + (ecy - pcy) ^ 2

            ' 当たり判定（距離が自機半径と隕石半径の和以下なら衝突）
            If dis < (PW / 2 + meteorDiameter / 2) ^ 2 Then
                hitFlg = True ' 当たった
                Exit For
            End If
        Next
    End Sub


    Private Sub pBase_Click(sender As Object, e As EventArgs) Handles pBase.Click
        If titleFlg AndAlso msgcnt > 20 Then
            msgcnt = 0
            titleFlg = False
            Exit Sub
        End If
        If msgcnt > 100 Then
            gameInit()
        Else
            ' 新しいビームを作成してリストに追加
            Dim newBeam As New Beam With {
            .X = Cpos.X + (PW / 2),  ' 自機の中心から発射
            .Y = Cpos.Y + (PH / 2),  ' 自機のY座標
            .Active = True
        }
            beams.Add(newBeam)
        End If
    End Sub
    'タイトル表示
    Private Sub titleDisp()
        msgcnt += 1
        g.DrawImage(pBG.Image, New Rectangle(0, 0, 680, 520))
        ' タイトル画像のサイズ
        Dim titleWidth As Integer = 350
        Dim titleHeight As Integer = 60

        ' タイトル画像を中央に配置するための座標計算
        Dim titleX As Integer = (680 - titleWidth) / 2
        Dim titleY As Integer = (520 - titleHeight) / 2

        g.DrawImage(pTitle.Image, New Rectangle(titleX, titleY, titleWidth, titleHeight))

        ' その他のタイトル関連の描画（必要に応じて）
        If (msgcnt Mod 60) > 20 Then
            ' この部分も中央に配置する場合は座標を計算
            Dim subtitleWidth As Integer = 271
            Dim subtitleHeight As Integer = 26
            Dim subtitleX As Integer = (680 - subtitleWidth) / 2
            Dim subtitleY As Integer = titleY + titleHeight + 10 ' タイトルの下に少し間隔を空ける

            g.DrawImage(pMsg.Image, New Rectangle(subtitleX, subtitleY, subtitleWidth, subtitleHeight))
        End If
        pBase.Image = canvas
    End Sub
    Private Sub SaveHighScore()
        'ハイスコアを表示させる
        System.IO.File.WriteAllText("highscore.txt", highScore.ToString())
    End Sub
    Private Sub LoadHighScore()
        If System.IO.File.Exists("highscore.txt") Then
            highScore = Long.Parse(System.IO.File.ReadAllText("highscore.txt"))
        Else
            highScore = 0
        End If
    End Sub
    Private Sub UpdateHighScore()
        If score > highScore Then
            highScore = score
            SaveHighScore()
        End If
    End Sub
    Private Sub DisplayHighScore()
        g.DrawString("HIGH SCORE:" & highScore.ToString(), myFont, Brushes.White, 10, 50)
    End Sub

End Class
