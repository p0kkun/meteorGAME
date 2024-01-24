<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        pBG = New PictureBox()
        pMeteor = New PictureBox()
        pBase = New PictureBox()
        pExp = New PictureBox()
        pGameover = New PictureBox()
        pMsg = New PictureBox()
        pPlayer = New PictureBox()
        pTitle = New PictureBox()
        Timer1 = New Timer(components)
        pBeam = New PictureBox()
        CType(pBG, ComponentModel.ISupportInitialize).BeginInit()
        CType(pMeteor, ComponentModel.ISupportInitialize).BeginInit()
        CType(pBase, ComponentModel.ISupportInitialize).BeginInit()
        CType(pExp, ComponentModel.ISupportInitialize).BeginInit()
        CType(pGameover, ComponentModel.ISupportInitialize).BeginInit()
        CType(pMsg, ComponentModel.ISupportInitialize).BeginInit()
        CType(pPlayer, ComponentModel.ISupportInitialize).BeginInit()
        CType(pTitle, ComponentModel.ISupportInitialize).BeginInit()
        CType(pBeam, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pBG
        ' 
        pBG.Image = My.Resources.Resources.p_bg
        pBG.Location = New Point(254, 65)
        pBG.Name = "pBG"
        pBG.Size = New Size(198, 204)
        pBG.TabIndex = 0
        pBG.TabStop = False
        ' 
        ' pMeteor
        ' 
        pMeteor.Image = My.Resources.Resources.p_meteor
        pMeteor.Location = New Point(342, 80)
        pMeteor.Name = "pMeteor"
        pMeteor.Size = New Size(72, 72)
        pMeteor.TabIndex = 1
        pMeteor.TabStop = False
        ' 
        ' pBase
        ' 
        pBase.Location = New Point(-8, -7)
        pBase.Name = "pBase"
        pBase.Size = New Size(1244, 638)
        pBase.TabIndex = 2
        pBase.TabStop = False
        ' 
        ' pExp
        ' 
        pExp.Image = My.Resources.Resources.p_explosion
        pExp.Location = New Point(173, 28)
        pExp.Name = "pExp"
        pExp.Size = New Size(100, 50)
        pExp.TabIndex = 3
        pExp.TabStop = False
        ' 
        ' pGameover
        ' 
        pGameover.Image = My.Resources.Resources.p_gameover
        pGameover.Location = New Point(173, 102)
        pGameover.Name = "pGameover"
        pGameover.Size = New Size(100, 50)
        pGameover.TabIndex = 4
        pGameover.TabStop = False
        ' 
        ' pMsg
        ' 
        pMsg.Image = My.Resources.Resources.p_msg
        pMsg.Location = New Point(148, 158)
        pMsg.Name = "pMsg"
        pMsg.Size = New Size(100, 50)
        pMsg.TabIndex = 5
        pMsg.TabStop = False
        ' 
        ' pPlayer
        ' 
        pPlayer.Image = My.Resources.Resources.p_player
        pPlayer.Location = New Point(67, 183)
        pPlayer.Name = "pPlayer"
        pPlayer.Size = New Size(100, 50)
        pPlayer.TabIndex = 6
        pPlayer.TabStop = False
        ' 
        ' pTitle
        ' 
        pTitle.Image = My.Resources.Resources.p_title
        pTitle.Location = New Point(240, 131)
        pTitle.Name = "pTitle"
        pTitle.Size = New Size(100, 50)
        pTitle.TabIndex = 7
        pTitle.TabStop = False
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 20
        ' 
        ' pBeam
        ' 
        pBeam.Image = CType(resources.GetObject("pBeam.Image"), Image)
        pBeam.Location = New Point(346, 39)
        pBeam.Name = "pBeam"
        pBeam.Size = New Size(106, 108)
        pBeam.TabIndex = 8
        pBeam.TabStop = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(664, 481)
        Controls.Add(pBeam)
        Controls.Add(pTitle)
        Controls.Add(pPlayer)
        Controls.Add(pMsg)
        Controls.Add(pGameover)
        Controls.Add(pExp)
        Controls.Add(pBase)
        Controls.Add(pMeteor)
        Controls.Add(pBG)
        Name = "Form1"
        Text = "Meteor"
        CType(pBG, ComponentModel.ISupportInitialize).EndInit()
        CType(pMeteor, ComponentModel.ISupportInitialize).EndInit()
        CType(pBase, ComponentModel.ISupportInitialize).EndInit()
        CType(pExp, ComponentModel.ISupportInitialize).EndInit()
        CType(pGameover, ComponentModel.ISupportInitialize).EndInit()
        CType(pMsg, ComponentModel.ISupportInitialize).EndInit()
        CType(pPlayer, ComponentModel.ISupportInitialize).EndInit()
        CType(pTitle, ComponentModel.ISupportInitialize).EndInit()
        CType(pBeam, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pBG As PictureBox
    Friend WithEvents pMeteor As PictureBox
    Friend WithEvents pBase As PictureBox
    Friend WithEvents pExp As PictureBox
    Friend WithEvents pGameover As PictureBox
    Friend WithEvents pMsg As PictureBox
    Friend WithEvents pPlayer As PictureBox
    Friend WithEvents pTitle As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents pBeam As PictureBox

End Class
