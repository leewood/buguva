object Form1: TForm1
  Left = 106
  Top = 161
  Width = 1093
  Height = 554
  AutoSize = True
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = 'Bibliotekos sistema'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Button2: TButton
    Left = 8
    Top = 0
    Width = 75
    Height = 25
    Caption = 'Prisijungti'
    TabOrder = 0
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 88
    Top = 0
    Width = 75
    Height = 25
    Cancel = True
    Caption = 'Atsijungti'
    TabOrder = 1
    Visible = False
    OnClick = Button3Click
  end
  object PageControl1: TPageControl
    Left = 8
    Top = 32
    Width = 1073
    Height = 465
    ActivePage = TabSheet2
    TabOrder = 2
    object TabSheet1: TTabSheet
      Caption = 'Info'
      object Label4: TLabel
        Left = 8
        Top = 24
        Width = 888
        Height = 148
        Caption = 
          'Jei norite gauti daugiau funkcij'#371', prisijunkite prie sistemos. T' +
          'ai galite padaryti paspaud'#281' mygtuk'#261' "Prisijungti". Jei neturite ' +
          'vartotojo '#353'ioje sistemoje, galite u'#382'siregistruoti paspaud'#281' mygtu' +
          'k'#261' "Registruotis"'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clRed
        Font.Height = -32
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        WordWrap = True
      end
      object Button4: TButton
        Left = 8
        Top = 184
        Width = 75
        Height = 25
        Caption = 'Registruotis'
        TabOrder = 0
        OnClick = Button4Click
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Knyg'#371' per'#382'i'#363'ra'
      ImageIndex = 1
      object Label2: TLabel
        Left = 0
        Top = 8
        Width = 63
        Height = 13
        Caption = 'Pavadinimas:'
      end
      object Label3: TLabel
        Left = 144
        Top = 8
        Width = 28
        Height = 13
        Caption = 'ISBN:'
      end
      object DBText8: TDBText
        Left = 672
        Top = 24
        Width = 65
        Height = 17
        DataField = 'nr'
        DataSource = DataModule2.DataSource8
        Visible = False
      end
      object Edit1: TEdit
        Left = 0
        Top = 24
        Width = 121
        Height = 21
        TabOrder = 0
      end
      object Edit2: TEdit
        Left = 144
        Top = 24
        Width = 121
        Height = 21
        TabOrder = 1
      end
      object Button1: TButton
        Left = 376
        Top = 16
        Width = 105
        Height = 25
        Caption = 'Atnaujinti / Filtruoti'
        TabOrder = 2
        OnClick = Button1Click
      end
      object DBGrid1: TDBGrid
        Left = 8
        Top = 76
        Width = 889
        Height = 361
        DataSource = DataModule2.DataSource1
        TabOrder = 3
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        Columns = <
          item
            Expanded = False
            FieldName = 'Tipas'
            Width = 83
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ISBN'
            Width = 119
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Pavadinimas'
            Width = 358
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Viso'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Paimta'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Nepaimta'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Rezervuota'
            Visible = True
          end>
      end
      object Button6: TButton
        Left = 488
        Top = 16
        Width = 75
        Height = 25
        Caption = 'Rezervuoti'
        TabOrder = 4
        OnClick = Button6Click
      end
      object Button7: TButton
        Left = 576
        Top = 16
        Width = 75
        Height = 25
        Caption = 'Pla'#269'iau'
        TabOrder = 5
        OnClick = Button7Click
      end
      object CheckBox1: TCheckBox
        Left = 280
        Top = 16
        Width = 97
        Height = 17
        Caption = 'CD'
        Checked = True
        State = cbChecked
        TabOrder = 6
      end
      object CheckBox2: TCheckBox
        Left = 280
        Top = 40
        Width = 97
        Height = 17
        Caption = 'Knygos'
        Checked = True
        State = cbChecked
        TabOrder = 7
      end
      object Button11: TButton
        Left = 912
        Top = 104
        Width = 105
        Height = 25
        Caption = 'Paimta'
        TabOrder = 8
        Visible = False
        OnClick = Button11Click
      end
      object Button17: TButton
        Left = 912
        Top = 136
        Width = 105
        Height = 25
        Caption = 'Prid'#279'ti nauj'#261' knyg'#261
        TabOrder = 9
        OnClick = Button17Click
      end
      object Button18: TButton
        Left = 912
        Top = 168
        Width = 105
        Height = 25
        Caption = 'Prid'#279'ti egzempliori'#371
        TabOrder = 10
        OnClick = Button18Click
      end
    end
    object TabSheet3: TTabSheet
      Caption = 'Mano knygos'
      ImageIndex = 2
      object Label1: TLabel
        Left = 16
        Top = 16
        Width = 77
        Height = 13
        Caption = 'Paimtos knygos:'
      end
      object Label15: TLabel
        Left = 8
        Top = 216
        Width = 100
        Height = 13
        Caption = 'Rezervuotos knygos:'
      end
      object DBGrid2: TDBGrid
        Left = 8
        Top = 32
        Width = 745
        Height = 177
        DataSource = DataModule2.DataSource8
        TabOrder = 0
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        Columns = <
          item
            Expanded = False
            FieldName = 'nr'
            Width = 36
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ISBN'
            Width = 100
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Pavadinimas'
            Width = 252
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Tipas'
            Width = 74
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'paimta'
            Width = 98
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'grazinti'
            Width = 105
            Visible = True
          end>
      end
      object Button8: TButton
        Left = 760
        Top = 32
        Width = 75
        Height = 25
        Caption = 'Pla'#269'iau'
        TabOrder = 1
        OnClick = Button8Click
      end
      object DBGrid3: TDBGrid
        Left = 8
        Top = 232
        Width = 633
        Height = 201
        DataSource = DataModule2.DataSource9
        TabOrder = 2
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        Columns = <
          item
            Expanded = False
            FieldName = 'nr'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ISBN'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Pavadinimas'
            Width = 268
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Tipas'
            Width = 61
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'data'
            Width = 89
            Visible = True
          end>
      end
      object Button9: TButton
        Left = 656
        Top = 232
        Width = 113
        Height = 25
        Caption = 'Pla'#269'iau'
        TabOrder = 3
        OnClick = Button9Click
      end
      object Button10: TButton
        Left = 656
        Top = 264
        Width = 113
        Height = 25
        Caption = 'At'#353'aukti rezervavim'#261
        TabOrder = 4
        OnClick = Button10Click
      end
    end
    object TabSheet4: TTabSheet
      Caption = 'Mano info'
      ImageIndex = 3
      object DBText1: TDBText
        Left = 72
        Top = 40
        Width = 65
        Height = 17
        DataField = 'vardas'
        DataSource = DataModule2.DataSource5
      end
      object DBText2: TDBText
        Left = 72
        Top = 88
        Width = 65
        Height = 17
        DataField = 'pavarde'
        DataSource = DataModule2.DataSource5
      end
      object DBText3: TDBText
        Left = 72
        Top = 136
        Width = 89
        Height = 17
        DataField = 'ak'
        DataSource = DataModule2.DataSource5
      end
      object DBText4: TDBText
        Left = 72
        Top = 184
        Width = 65
        Height = 17
        DataField = 'gimimas'
        DataSource = DataModule2.DataSource5
      end
      object DBText5: TDBText
        Left = 72
        Top = 288
        Width = 65
        Height = 17
        DataField = 'leidimai'
        DataSource = DataModule2.DataSource5
      end
      object Label5: TLabel
        Left = 72
        Top = 16
        Width = 36
        Height = 13
        Caption = 'Vardas:'
      end
      object Label6: TLabel
        Left = 72
        Top = 64
        Width = 43
        Height = 13
        Caption = 'Pavard'#279':'
      end
      object Label7: TLabel
        Left = 72
        Top = 112
        Width = 72
        Height = 13
        Caption = 'Asmens kodas:'
      end
      object Label8: TLabel
        Left = 72
        Top = 160
        Width = 61
        Height = 13
        Caption = 'Gimimo data:'
      end
      object Label9: TLabel
        Left = 72
        Top = 208
        Width = 41
        Height = 13
        Caption = 'Adresas:'
      end
      object Label10: TLabel
        Left = 72
        Top = 264
        Width = 75
        Height = 13
        Caption = 'Vartotojo teis'#279's:'
      end
      object Label11: TLabel
        Left = 192
        Top = 16
        Width = 59
        Height = 13
        Caption = 'Vartotojo ID:'
      end
      object DBText6: TDBText
        Left = 192
        Top = 32
        Width = 65
        Height = 17
        DataField = 'nr'
        DataSource = DataModule2.DataSource5
      end
      object DBText7: TDBText
        Left = 360
        Top = 24
        Width = 65
        Height = 17
        DataField = 'slaptazodis'
        DataSource = DataModule2.DataSource5
        Visible = False
      end
      object DBEdit1: TDBEdit
        Left = 144
        Top = 200
        Width = 121
        Height = 21
        DataField = 'adresas'
        DataSource = DataModule2.DataSource5
        TabOrder = 0
        Visible = False
      end
      object GroupBox1: TGroupBox
        Left = 312
        Top = 40
        Width = 185
        Height = 185
        Caption = 'Keisti slapta'#382'od'#303
        TabOrder = 1
        object Label12: TLabel
          Left = 24
          Top = 16
          Width = 88
          Height = 13
          Caption = 'Senas slapta'#382'odis:'
        end
        object Label13: TLabel
          Left = 24
          Top = 56
          Width = 91
          Height = 13
          Caption = 'Naujas slapta'#382'odis:'
        end
        object Label14: TLabel
          Left = 24
          Top = 96
          Width = 45
          Height = 13
          Caption = 'Pakartoti:'
        end
        object Edit3: TEdit
          Left = 24
          Top = 32
          Width = 121
          Height = 21
          PasswordChar = '*'
          TabOrder = 0
        end
        object Edit4: TEdit
          Left = 24
          Top = 72
          Width = 121
          Height = 21
          PasswordChar = '*'
          TabOrder = 1
        end
        object Edit5: TEdit
          Left = 24
          Top = 112
          Width = 121
          Height = 21
          PasswordChar = '*'
          TabOrder = 2
        end
        object Button5: TButton
          Left = 48
          Top = 144
          Width = 75
          Height = 25
          Caption = 'Keisti'
          TabOrder = 3
          OnClick = Button5Click
        end
      end
      object Edit6: TEdit
        Left = 72
        Top = 232
        Width = 105
        Height = 21
        TabOrder = 2
        Text = 'Edit6'
        OnChange = Edit6Change
      end
    end
    object TabSheet5: TTabSheet
      Caption = 'Bibliotekininki'#371' skiltis'
      ImageIndex = 4
      object Label16: TLabel
        Left = 16
        Top = 16
        Width = 51
        Height = 13
        Caption = 'Skaitytojai:'
      end
      object Label17: TLabel
        Left = 536
        Top = 8
        Width = 144
        Height = 13
        Caption = 'Skaitytojo rezervuotos knygos:'
      end
      object Label18: TLabel
        Left = 448
        Top = 232
        Width = 125
        Height = 13
        Caption = 'Skaitytojo paimtos knygos:'
      end
      object DBGrid4: TDBGrid
        Left = 0
        Top = 32
        Width = 449
        Height = 385
        DataSource = DataModule2.DataSource11
        TabOrder = 0
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        OnCellClick = DBGrid4CellClick
        OnMouseDown = DBGrid4MouseDown
        Columns = <
          item
            Expanded = False
            FieldName = 'nr'
            Width = 48
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ak'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'vardas'
            Width = 88
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'pavarde'
            Width = 64
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'leidimai'
            Width = 65
            Visible = True
          end>
      end
      object Button12: TButton
        Left = 456
        Top = 32
        Width = 81
        Height = 25
        Caption = 'Pa'#353'alinti'
        TabOrder = 1
        OnClick = Button12Click
      end
      object Button13: TButton
        Left = 456
        Top = 64
        Width = 81
        Height = 25
        Caption = 'Padidinti teises'
        TabOrder = 2
        OnClick = Button13Click
      end
      object Button14: TButton
        Left = 456
        Top = 96
        Width = 81
        Height = 25
        Caption = 'Suma'#382'inti teises'
        TabOrder = 3
        OnClick = Button14Click
      end
      object DBGrid5: TDBGrid
        Left = 448
        Top = 248
        Width = 609
        Height = 185
        DataSource = DataModule2.DataSource12
        TabOrder = 4
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        Columns = <
          item
            Expanded = False
            FieldName = 'nr'
            Width = 43
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ISBN'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Pavadinimas'
            Width = 207
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Tipas'
            Width = 44
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'paimta'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'grazinti'
            Width = 63
            Visible = True
          end>
      end
      object DBGrid6: TDBGrid
        Left = 544
        Top = 24
        Width = 513
        Height = 193
        DataSource = DataModule2.DataSource13
        TabOrder = 5
        TitleFont.Charset = DEFAULT_CHARSET
        TitleFont.Color = clWindowText
        TitleFont.Height = -11
        TitleFont.Name = 'MS Sans Serif'
        TitleFont.Style = []
        Columns = <
          item
            Expanded = False
            FieldName = 'nr'
            Width = 41
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'ISBN'
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Pavadinimas'
            Width = 64
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'Tipas'
            Width = 43
            Visible = True
          end
          item
            Expanded = False
            FieldName = 'data'
            Visible = True
          end>
      end
      object Button15: TButton
        Left = 976
        Top = 224
        Width = 75
        Height = 25
        Caption = 'Gr'#261#382'ino'
        TabOrder = 6
        OnClick = Button15Click
      end
      object Button16: TButton
        Left = 976
        Top = 0
        Width = 75
        Height = 25
        Caption = 'Pa'#279'm'#279
        TabOrder = 7
        OnClick = Button16Click
      end
      object DBEdit2: TDBEdit
        Left = 456
        Top = 128
        Width = 65
        Height = 21
        DataField = 'nr'
        DataSource = DataModule2.DataSource14
        TabOrder = 8
        Visible = False
      end
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 505
    Width = 1085
    Height = 19
    Panels = <
      item
        Width = 50
      end>
  end
  object DBEdit3: TDBEdit
    Left = 424
    Top = 0
    Width = 121
    Height = 21
    DataField = 'max(nr)'
    DataSource = DataModule2.DataSource19
    TabOrder = 4
    Visible = False
  end
  object Timer1: TTimer
    Enabled = False
    OnTimer = Timer1Timer
    Left = 280
    Top = 8
  end
end
