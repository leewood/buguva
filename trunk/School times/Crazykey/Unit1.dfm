object Form1: TForm1
  Left = 192
  Top = 107
  Width = 237
  Height = 170
  Caption = 'CrazyKey'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDblClick = FormDblClick
  OnDeactivate = FormDeactivate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 16
    Top = 72
    Width = 34
    Height = 13
    Caption = 'Melody'
  end
  object Edit1: TEdit
    Left = 16
    Top = 96
    Width = 121
    Height = 21
    TabOrder = 0
    Text = '123'
  end
  object Button1: TButton
    Left = 144
    Top = 80
    Width = 75
    Height = 25
    Caption = 'Play'
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 144
    Top = 112
    Width = 75
    Height = 25
    Caption = 'Stop'
    TabOrder = 2
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 144
    Top = 48
    Width = 75
    Height = 25
    Caption = 'Clear'
    TabOrder = 3
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 8
    Top = 0
    Width = 75
    Height = 25
    Caption = 'Save'
    TabOrder = 4
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 8
    Top = 32
    Width = 75
    Height = 25
    Caption = 'Load'
    TabOrder = 5
    OnClick = Button5Click
  end
  object KeyState1: TKeyState
    Left = 88
    Top = 40
  end
  object Timer1: TTimer
    Interval = 100
    OnTimer = Timer1Timer
    Left = 88
    Top = 8
  end
  object SaveDialog1: TSaveDialog
    Filter = '*.mz|*.mz'
    Left = 112
    Top = 40
  end
  object OpenDialog1: TOpenDialog
    Filter = '*.mz|*.mz'
    Left = 112
    Top = 8
  end
  object TBNArea1: TTBNArea
    Copyright = #169' R. N'#228'gele 1997 - 99, Version 3.1'
    Enabled = False
    Icon.Data = {
      0000010001002020100000000000E80200001600000028000000200000004000
      0000010004000000000080020000000000000000000000000000000000000000
      000000008000008000000080800080000000800080008080000080808000C0C0
      C0000000FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000008888800000000000000000000000008844444880000000000000000
      0000008447777744800000000000000000000844444444444800000000000000
      0000844444444444448000000000000000008444444444444480000000000000
      000844444E444444444800000000000000084444E44444444448000000000000
      0008444E6E44444444480000000000000008444CECECCCCC4448000000000000
      00084CCE6ECCCCCCCC4800000000000000008CCCE6ECCCCCCC80000000000000
      000088FCCECCCCCCF8800000000000000000088F8F8F8F8F8800000000000000
      00000088FFF8F8F88000000000000000000000088FFF8F880000000000000000
      0000000088FFF8800000000000000000000000007F8F8F700000000000000000
      0000000008FFF800000000000000000000000000088F88000000000000000000
      0000000008FFF800000000000000000000000000088F88000000000000000000
      0000000008FFF800000000000000000000000000088F88000000000000000000
      0000000008F87700000000000000000000000007888888870000000000000000
      0000000887777788000000000000000000000007788888770000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FFFFFFF83FFFFFE00FFFFFC007FFFF8003FFFF0001FFFE0000FFFE0000FFFC00
      007FFC00007FFC00007FFC00007FFC00007FFE0000FFFE0000FFFF0001FFFF80
      03FFFFC007FFFFE00FFFFFE00FFFFFF01FFFFFF01FFFFFF01FFFFFF01FFFFFF0
      1FFFFFF01FFFFFE00FFFFFC007FFFFC007FFFFC007FFFFE00FFFFFFFFFFF}
    PopupMenuR = PopupMenu1
    OnDblClick = TBNArea1DblClick
    Left = 144
    Top = 8
  end
  object PopupMenu1: TPopupMenu
    Left = 176
    Top = 8
    object Show1: TMenuItem
      Caption = 'Show'
      OnClick = Show1Click
    end
    object Close1: TMenuItem
      Caption = 'Close'
      OnClick = Close1Click
    end
  end
end
