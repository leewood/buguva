object Form1: TForm1
  Left = 213
  Top = 100
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'VDP'
  ClientHeight = 425
  ClientWidth = 536
  Color = clWhite
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Icon.Data = {
    0000010001002020100000000000E80200001600000028000000200000004000
    0000010004000000000080020000000000000000000000000000000000000000
    000000008000008000000080800080000000800080008080000080808000C0C0
    C0000000FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF000000
    0000000000000000000000000000000000000000007777770000000000000000
    0000000000000007000000000000000000000000000000070000000000000000
    0000000000777007000000000000000000077070007770070000700000000000
    0077000700787000000007000000000007708000077877000070007000000000
    07088807777777770777000700000000008F88877FFFFF077887700700000000
    00088888F88888FF08870070000000000000880888877778F070007000000007
    77088888880007778F770077777000700008F088007777077F07000000700700
    008F08880800077778F7700000700708888F0880F08F807078F7777700700708
    F88F0780F070F07078F7887700700708888F0780F077807088F7777700700700
    008F0788FF00080888F77000007000000008F0780FFFF0088F77007000000000
    0008F07788000888887700700000000000008F07788888880870007000000000
    00088FF0077788088887000700000000008F888FF00000F87887700700000000
    0708F8088FFFFF88078700700000000007708000088888000070070000000000
    0077007000888007000070000000000000077700008F80070007000000000000
    0000000000888007000000000000000000000000000000070000000000000000
    000000000777777700000000000000000000000000000000000000000000FFFF
    FFFFFFFC0FFFFFFC0FFFFFF80FFFFFF80FFFFE180E7FFC00043FF800001FF800
    000FF800000FFC00001FFE00001FE0000001C000000180000001800000018000
    00018000000180000001FC00001FFC00001FFE00001FFC00000FF800000FF800
    001FF800003FFC180C7FFE380EFFFFF80FFFFFF80FFFFFF80FFFFFFFFFFF}
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 416
    Top = 280
    Width = 62
    Height = 14
    Caption = 'Pirmumo eilë:'
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 416
    Top = 296
    Width = 38
    Height = 13
    Caption = 'Sujungti'
  end
  object Label3: TLabel
    Left = 408
    Top = 376
    Width = 110
    Height = 28
    Caption = 'Eksperimentinë versija (C) Karolis Uosis 2004'
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
    WordWrap = True
  end
  object StringGrid1: TStringGrid
    Left = 0
    Top = 0
    Width = 345
    Height = 417
    ColCount = 3
    FixedCols = 0
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
    TabOrder = 0
    ColWidths = (
      128
      156
      30)
  end
  object Button5: TButton
    Left = 448
    Top = 40
    Width = 75
    Height = 25
    Caption = 'Atidaryti'
    TabOrder = 1
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 448
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Dëlioti'
    TabOrder = 2
    OnClick = Button6Click
  end
  object RadioGroup1: TRadioGroup
    Left = 416
    Top = 160
    Width = 113
    Height = 105
    Caption = 'Bûdas'
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ItemIndex = 0
    Items.Strings = (
      'Nuo priekio'
      'Nuo galo'
      'Pagal ilgá'
      'Pagal priesagà')
    ParentFont = False
    TabOrder = 3
  end
  object RadioGroup2: TRadioGroup
    Left = 424
    Top = 80
    Width = 105
    Height = 73
    Caption = 'Tipas'
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ItemIndex = 0
    Items.Strings = (
      'Maþëjanèiai'
      'Didëjanèiai')
    ParentFont = False
    TabOrder = 4
  end
  object Button1: TButton
    Left = 360
    Top = 40
    Width = 75
    Height = 25
    Caption = 'Iðsaugoti'
    TabOrder = 5
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 416
    Top = 320
    Width = 75
    Height = 25
    Caption = 'Keisti'
    Font.Charset = BALTIC_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
    TabOrder = 6
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 360
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Þodynai'
    TabOrder = 7
    OnClick = Button3Click
  end
  object OpenDialog1: TOpenDialog
    Filter = 'Sàraðai|*.txt'
    Title = 'Atidaryti sàraðà'
    Left = 352
    Top = 120
  end
  object SaveDialog1: TSaveDialog
    Filter = 'Sàraðai|*.txt'
    Title = 'Iðsaugoti sàraðà'
    Left = 352
    Top = 152
  end
end
