object Form1: TForm1
  Left = 192
  Top = 107
  Width = 696
  Height = 480
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Button1: TButton
    Left = 72
    Top = 104
    Width = 137
    Height = 25
    Caption = 'I hate start button'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 72
    Top = 144
    Width = 137
    Height = 25
    Caption = 'I love start button'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 240
    Top = 104
    Width = 89
    Height = 25
    Caption = 'I hate taskbar'
    TabOrder = 2
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 336
    Top = 104
    Width = 75
    Height = 25
    Caption = 'I hate tray'
    TabOrder = 3
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 240
    Top = 144
    Width = 75
    Height = 25
    Caption = 'I love taskbar'
    TabOrder = 4
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 336
    Top = 144
    Width = 75
    Height = 25
    Caption = 'I love tray'
    TabOrder = 5
    OnClick = Button6Click
  end
  object Button7: TButton
    Left = 440
    Top = 112
    Width = 75
    Height = 25
    Caption = 'Button7'
    TabOrder = 6
    OnClick = Button7Click
  end
  object Edit1: TEdit
    Left = 304
    Top = 208
    Width = 121
    Height = 21
    TabOrder = 7
    Text = 'Edit1'
  end
  object WinAccess1: TWinAccess
    StartButton = asVisible
    TrayBar = asVisible
    TrayNotify = asVisible
    AppSwitch = asHiden
    CapsLock = kbOn
    NumLock = kbOn
    ScrollLock = kbOff
    SystemKey = kbOn
    HoldNumLock = False
    HoldScrollLock = False
    HoldCapsLock = False
    Left = 56
    Top = 48
  end
end
