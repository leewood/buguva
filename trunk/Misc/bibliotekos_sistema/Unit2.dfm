object DataModule2: TDataModule2
  OldCreateOrder = False
  Left = 202
  Top = 342
  Height = 292
  Width = 1040
  object Query3: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'SELECT DISTINCT knyga.kalba'
      'FROM biblio.knyga')
    Left = 8
    Top = 16
  end
  object Table1: TTable
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    FieldDefs = <
      item
        Name = 'ISBN'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 13
      end
      item
        Name = 'Pavadinimas'
        Attributes = [faRequired]
        DataType = ftString
        Size = 32
      end
      item
        Name = 'Leidykla'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Metai'
        Attributes = [faRequired]
        DataType = ftSmallint
      end
      item
        Name = 'Puslapiai'
        Attributes = [faRequired]
        DataType = ftSmallint
      end
      item
        Name = 'Verte'
        DataType = ftFloat
      end
      item
        Name = 'Kalba'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Sritis'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Autorius'
        Attributes = [faRequired]
        DataType = ftInteger
      end>
    IndexDefs = <
      item
        Name = 'PRIMARY'
        Fields = 'ISBN'
        Options = [ixUnique]
      end
      item
        Name = 'AutKnyg'
        Fields = 'Autorius'
      end>
    IndexName = 'PRIMARY'
    MasterSource = DataSource1
    StoreDefs = True
    TableName = 'knyga'
    Left = 224
    Top = 16
  end
  object Query1: TQuery
    AutoRefresh = True
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'SELECT egzemplioriai.tipas, egzemplioriai.isbn, egzemplioriai.pa' +
        'vadinimas, egzemplioriai.viso, egzemplioriai.paimta, egzempliori' +
        'ai.nepaimta, egzemplioriai.rezervuota FROM biblio.egzemplioriai')
    Left = 72
    Top = 72
    object Query1Tipas: TStringField
      FieldName = 'Tipas'
      Size = 5
    end
    object Query1ISBN: TStringField
      FieldName = 'ISBN'
      FixedChar = True
      Size = 13
    end
    object Query1Pavadinimas: TStringField
      FieldName = 'Pavadinimas'
      Size = 32
    end
    object Query1Viso: TFloatField
      FieldName = 'Viso'
    end
    object Query1Paimta: TFloatField
      FieldName = 'Paimta'
    end
    object Query1Nepaimta: TFloatField
      FieldName = 'Nepaimta'
    end
    object Query1Rezervuota: TFloatField
      FieldName = 'Rezervuota'
    end
  end
  object Database1: TDatabase
    AliasName = 'Mydb'
    Connected = True
    DatabaseName = 'MyDb'
    LoginPrompt = False
    SessionName = 'mysession'
    Left = 136
    Top = 72
  end
  object DataSource1: TDataSource
    DataSet = Query1
    Left = 16
    Top = 64
  end
  object Query2: TQuery
    AutoCalcFields = False
    AutoRefresh = True
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    ParamCheck = False
    SQL.Strings = (
      'SELECT DISTINCT knyga.kalba'
      'FROM biblio.knyga')
    Left = 64
    Top = 24
  end
  object DataSource2: TDataSource
    DataSet = Query2
    Left = 152
    Top = 24
  end
  object Table2: TTable
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    FieldDefs = <
      item
        Name = 'ISBN'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 13
      end
      item
        Name = 'Pavadinimas'
        Attributes = [faRequired]
        DataType = ftString
        Size = 32
      end
      item
        Name = 'Leidykla'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Metai'
        Attributes = [faRequired]
        DataType = ftSmallint
      end
      item
        Name = 'Puslapiai'
        Attributes = [faRequired]
        DataType = ftSmallint
      end
      item
        Name = 'Verte'
        DataType = ftFloat
      end
      item
        Name = 'Kalba'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Sritis'
        Attributes = [faRequired, faFixed]
        DataType = ftString
        Size = 12
      end
      item
        Name = 'Autorius'
        Attributes = [faRequired]
        DataType = ftInteger
      end>
    IndexDefs = <
      item
        Name = 'PRIMARY'
        Fields = 'ISBN'
        Options = [ixUnique]
      end
      item
        Name = 'AutKnyg'
        Fields = 'Autorius'
      end>
    IndexFieldNames = 'ISBN'
    MasterSource = DataSource2
    StoreDefs = True
    TableName = 'knyga'
    Left = 184
    Top = 16
  end
  object DataSource3: TDataSource
    DataSet = Query3
    Left = 200
    Top = 72
  end
  object Query4: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    ParamCheck = False
    SQL.Strings = (
      'select slaptazodis from biblio.skaitytojas')
    Left = 264
    Top = 72
    object Query4slaptazodis: TStringField
      FieldName = 'slaptazodis'
      Size = 15
    end
  end
  object DataSource4: TDataSource
    DataSet = Query4
    Left = 272
    Top = 8
  end
  object Session1: TSession
    Active = True
    SessionName = 'mysession'
    Left = 104
    Top = 16
  end
  object Query5: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select nr, vardas, pavarde, ak, gimimas, adresas, leidimai, slap' +
        'tazodis from biblio.skaitytojas')
    Left = 312
    Top = 72
    object Query5nr: TIntegerField
      FieldName = 'nr'
    end
    object Query5vardas: TStringField
      FieldName = 'vardas'
      FixedChar = True
      Size = 12
    end
    object Query5pavarde: TStringField
      FieldName = 'pavarde'
    end
    object Query5ak: TStringField
      FieldName = 'ak'
      FixedChar = True
      Size = 11
    end
    object Query5gimimas: TDateField
      FieldName = 'gimimas'
    end
    object Query5adresas: TStringField
      FieldName = 'adresas'
      Size = 32
    end
    object Query5leidimai: TSmallintField
      FieldName = 'leidimai'
    end
    object Query5slaptazodis: TStringField
      FieldName = 'slaptazodis'
      Size = 15
    end
  end
  object DataSource5: TDataSource
    DataSet = Query5
    Left = 328
    Top = 8
  end
  object Query6: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'SELECT autorius.vardas, autorius.pavarde, knyga.isbn, knyga.pava' +
        'dinimas, knyga.leidykla, knyga.metai, knyga.puslapiai, knyga.kal' +
        'ba, knyga.sritis, egzemplioriai.viso, egzemplioriai.paimta, egze' +
        'mplioriai.nepaimta, egzemplioriai.rezervuota, knyga.verte FROM b' +
        'iblio.egzemplioriai, biblio.knyga, biblio.autorius where egzempl' +
        'ioriai.isbn = knyga.isbn and knyga.autorius = autorius.nr and kn' +
        'yga.isbn = :aisbn')
    Left = 368
    Top = 64
    ParamData = <
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end>
    object Query6vardas: TStringField
      FieldName = 'vardas'
      FixedChar = True
      Size = 12
    end
    object Query6pavarde: TStringField
      FieldName = 'pavarde'
    end
    object Query6isbn: TStringField
      FieldName = 'isbn'
      FixedChar = True
      Size = 13
    end
    object Query6pavadinimas: TStringField
      FieldName = 'pavadinimas'
      Size = 32
    end
    object Query6leidykla: TStringField
      FieldName = 'leidykla'
      FixedChar = True
      Size = 12
    end
    object Query6metai: TSmallintField
      FieldName = 'metai'
    end
    object Query6puslapiai: TSmallintField
      FieldName = 'puslapiai'
    end
    object Query6kalba: TStringField
      FieldName = 'kalba'
      FixedChar = True
      Size = 12
    end
    object Query6sritis: TStringField
      FieldName = 'sritis'
      FixedChar = True
      Size = 12
    end
    object Query6Viso: TFloatField
      FieldName = 'Viso'
    end
    object Query6Paimta: TFloatField
      FieldName = 'Paimta'
    end
    object Query6Nepaimta: TFloatField
      FieldName = 'Nepaimta'
    end
    object Query6Rezervuota: TFloatField
      FieldName = 'Rezervuota'
    end
    object Query6verte: TFloatField
      FieldName = 'verte'
    end
  end
  object DataSource6: TDataSource
    DataSet = Query6
    Left = 384
    Top = 8
  end
  object Query7: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'SELECT autorius.vardas, autorius.pavarde, cd.isbn, cd.pavadinima' +
        's, cd.data, cd.kalba, cd.sritis, egzemplioriai.viso, egzempliori' +
        'ai.paimta, egzemplioriai.nepaimta, egzemplioriai.rezervuota, cd.' +
        'verte FROM biblio.egzemplioriai, biblio.cd, biblio.autorius wher' +
        'e egzemplioriai.isbn = cd.isbn and cd.autorius = autorius.nr and' +
        ' cd.isbn = :aisbn')
    Left = 416
    Top = 72
    ParamData = <
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end>
    object Query7vardas: TStringField
      FieldName = 'vardas'
      FixedChar = True
      Size = 12
    end
    object Query7pavarde: TStringField
      FieldName = 'pavarde'
    end
    object Query7isbn: TStringField
      FieldName = 'isbn'
      FixedChar = True
      Size = 13
    end
    object Query7pavadinimas: TStringField
      FieldName = 'pavadinimas'
      Size = 32
    end
    object Query7data: TDateField
      FieldName = 'data'
    end
    object Query7kalba: TStringField
      FieldName = 'kalba'
      FixedChar = True
      Size = 12
    end
    object Query7sritis: TStringField
      FieldName = 'sritis'
      FixedChar = True
      Size = 12
    end
    object Query7Viso: TFloatField
      FieldName = 'Viso'
    end
    object Query7Paimta: TFloatField
      FieldName = 'Paimta'
    end
    object Query7Nepaimta: TFloatField
      FieldName = 'Nepaimta'
    end
    object Query7Rezervuota: TFloatField
      FieldName = 'Rezervuota'
    end
    object Query7verte: TFloatField
      FieldName = 'verte'
    end
  end
  object DataSource7: TDataSource
    DataSet = Query7
    Left = 440
  end
  object Query8: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select egzempliorius.nr, egzemplioriai.isbn, egzemplioriai.pavad' +
        'inimas, egzemplioriai.tipas, egzempliorius.paimta, egzempliorius' +
        '.grazinti from biblio.egzempliorius,  biblio.egzemplioriai where' +
        ' egzemplioriai.isbn = egzempliorius.isbn and skaitytojas =:askai' +
        't')
    Left = 464
    Top = 72
    ParamData = <
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
    object Query8nr: TIntegerField
      FieldName = 'nr'
    end
    object Query8ISBN: TStringField
      FieldName = 'ISBN'
      FixedChar = True
      Size = 13
    end
    object Query8Pavadinimas: TStringField
      FieldName = 'Pavadinimas'
      Size = 32
    end
    object Query8Tipas: TStringField
      FieldName = 'Tipas'
      Size = 5
    end
    object Query8paimta: TDateField
      FieldName = 'paimta'
    end
    object Query8grazinti: TDateField
      FieldName = 'grazinti'
    end
  end
  object DataSource8: TDataSource
    DataSet = Query8
    Left = 488
    Top = 16
  end
  object Query9: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'insert into biblio.rezervuota values (0, now(), :myid, :myegz)')
    Left = 520
    Top = 72
    ParamData = <
      item
        DataType = ftInteger
        Name = 'myid'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'myegz'
        ParamType = ptUnknown
      end>
  end
  object UpdateSQL1: TUpdateSQL
    InsertSQL.Strings = (
      'insert into biblio.rezervuota values (0, now(), :myid, :myegz)')
    Left = 552
    Top = 16
  end
  object Query10: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select rezervuota.nr, egzemplioriai.isbn, egzemplioriai.pavadini' +
        'mas, egzemplioriai.tipas, rezervuota.data from biblio.rezervuota' +
        ',  biblio.egzemplioriai where egzemplioriai.isbn = rezervuota.eg' +
        'zempliorius and rezervuotojas =:askait')
    Left = 568
    Top = 72
    ParamData = <
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
    object Query10nr: TIntegerField
      FieldName = 'nr'
    end
    object Query10ISBN: TStringField
      FieldName = 'ISBN'
      FixedChar = True
      Size = 13
    end
    object Query10Pavadinimas: TStringField
      FieldName = 'Pavadinimas'
      Size = 32
    end
    object Query10Tipas: TStringField
      FieldName = 'Tipas'
      Size = 5
    end
    object Query10data: TDateField
      FieldName = 'data'
    end
  end
  object DataSource9: TDataSource
    DataSet = Query10
    Left = 616
    Top = 16
  end
  object Query11: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'update biblio.skaitytojas set slaptazodis = :aslapt where nr = :' +
        'askait')
    Left = 616
    Top = 72
    ParamData = <
      item
        DataType = ftString
        Name = 'aslapt'
        ParamType = ptUnknown
      end
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
  end
  object Query12: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'update biblio.skaitytojas set adresas = :aadr where nr = :askait')
    Left = 664
    Top = 72
    ParamData = <
      item
        DataType = ftString
        Name = 'aadr'
        ParamType = ptUnknown
      end
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
  end
  object Query13: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'delete from biblio.rezervuota where biblio.rezervuota.nr = :anr')
    Left = 728
    Top = 72
    ParamData = <
      item
        DataType = ftInteger
        Name = 'anr'
        ParamType = ptUnknown
      end>
  end
  object Query14: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'insert into biblio.skaitytojas values (NULL, :aak, :avardas, :ap' +
        'avarde, :agimimas, :aadresas, 0, :aslaptazodis)')
    Left = 672
    Top = 16
    ParamData = <
      item
        DataType = ftString
        Name = 'aak'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'avardas'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'apavarde'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'agimimas'
        ParamType = ptUnknown
        Value = '2001-10-11'
      end
      item
        DataType = ftString
        Name = 'aadresas'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'aslaptazodis'
        ParamType = ptUnknown
      end>
  end
  object Query15: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      'select nr from biblio.skaitytojas where ak = :aak')
    Left = 720
    Top = 16
    ParamData = <
      item
        DataType = ftString
        Name = 'aak'
        ParamType = ptUnknown
      end>
    object Query15nr: TIntegerField
      FieldName = 'nr'
    end
  end
  object DataSource10: TDataSource
    DataSet = Query15
    Left = 792
    Top = 24
  end
  object Query16: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      'select nr, ak, vardas, pavarde, leidimai from biblio.skaitytojas')
    Left = 16
    Top = 136
    object Query16nr: TIntegerField
      FieldName = 'nr'
    end
    object Query16ak: TStringField
      FieldName = 'ak'
      FixedChar = True
      Size = 11
    end
    object Query16vardas: TStringField
      FieldName = 'vardas'
      FixedChar = True
      Size = 12
    end
    object Query16pavarde: TStringField
      FieldName = 'pavarde'
    end
    object Query16leidimai: TSmallintField
      FieldName = 'leidimai'
    end
  end
  object DataSource11: TDataSource
    DataSet = Query16
    Left = 72
    Top = 136
  end
  object Query17: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select egzempliorius.nr, egzemplioriai.isbn, egzemplioriai.pavad' +
        'inimas, egzemplioriai.tipas, egzempliorius.paimta, egzempliorius' +
        '.grazinti from biblio.egzempliorius,  biblio.egzemplioriai where' +
        ' egzemplioriai.isbn = egzempliorius.isbn and skaitytojas =:askai' +
        't')
    Left = 144
    Top = 136
    ParamData = <
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
    object IntegerField1: TIntegerField
      FieldName = 'nr'
    end
    object StringField1: TStringField
      FieldName = 'ISBN'
      FixedChar = True
      Size = 13
    end
    object StringField2: TStringField
      FieldName = 'Pavadinimas'
      Size = 32
    end
    object StringField3: TStringField
      FieldName = 'Tipas'
      Size = 5
    end
    object DateField1: TDateField
      FieldName = 'paimta'
    end
    object DateField2: TDateField
      FieldName = 'grazinti'
    end
  end
  object Query18: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select rezervuota.nr, egzemplioriai.isbn, egzemplioriai.pavadini' +
        'mas, egzemplioriai.tipas, rezervuota.data from biblio.rezervuota' +
        ',  biblio.egzemplioriai where egzemplioriai.isbn = rezervuota.eg' +
        'zempliorius and rezervuotojas =:askait')
    Left = 208
    Top = 136
    ParamData = <
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end>
    object IntegerField2: TIntegerField
      FieldName = 'nr'
    end
    object StringField4: TStringField
      FieldName = 'ISBN'
      FixedChar = True
      Size = 13
    end
    object StringField5: TStringField
      FieldName = 'Pavadinimas'
      Size = 32
    end
    object StringField6: TStringField
      FieldName = 'Tipas'
      Size = 5
    end
    object DateField3: TDateField
      FieldName = 'data'
    end
  end
  object DataSource12: TDataSource
    DataSet = Query17
    Left = 280
    Top = 136
  end
  object DataSource13: TDataSource
    DataSet = Query18
    Left = 360
    Top = 136
  end
  object Query19: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'update biblio.skaitytojas set leidimai = :aleid where nr = :aska' +
        'it')
    Left = 440
    Top = 136
    ParamData = <
      item
        DataType = ftUnknown
        Name = 'aleid'
        ParamType = ptUnknown
      end
      item
        DataType = ftUnknown
        Name = 'askait'
        ParamType = ptUnknown
      end>
  end
  object Query20: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      'delete from biblio.skaitytojas where nr = :askait')
    Left = 488
    Top = 136
    ParamData = <
      item
        DataType = ftUnknown
        Name = 'askait'
        ParamType = ptUnknown
      end>
  end
  object Query21: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'update biblio.egzempliorius set skaitytojas = null, paimta = nul' +
        'l, grazinti = null where nr = :anr;'
      ''
      ''
      '')
    Left = 552
    Top = 136
    ParamData = <
      item
        DataType = ftInteger
        Name = 'anr'
        ParamType = ptUnknown
      end>
  end
  object Query22: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select nr, isbn from biblio.egzempliorius where isbn = :aisbn an' +
        'd skaitytojas is null')
    Left = 616
    Top = 136
    ParamData = <
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end>
  end
  object DataSource14: TDataSource
    DataSet = Query22
    Left = 680
    Top = 136
  end
  object Query23: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'delete from biblio.rezervuota where nr = :anr')
    Left = 736
    Top = 136
    ParamData = <
      item
        DataType = ftInteger
        Name = 'anr'
        ParamType = ptUnknown
      end>
  end
  object Query24: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'update biblio.egzempliorius set skaitytojas = :askait, paimta = ' +
        'now(), grazinti = :adata where nr = :anr')
    Left = 784
    Top = 136
    ParamData = <
      item
        DataType = ftInteger
        Name = 'askait'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'adata'
        ParamType = ptUnknown
      end
      item
        DataType = ftInteger
        Name = 'anr'
        ParamType = ptUnknown
      end>
  end
  object Query25: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select distinct kalba from (select kalba from biblio.knyga union' +
        ' select kalba from biblio.cd) as tmp1')
    Left = 864
    Top = 24
    object Query25kalba: TStringField
      FieldName = 'kalba'
      FixedChar = True
      Size = 12
    end
  end
  object DataSource15: TDataSource
    DataSet = Query25
    Left = 928
    Top = 24
  end
  object Query26: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select distinct sritis from (select sritis from biblio.knyga uni' +
        'on select sritis from biblio.cd) as tmp1')
    Left = 848
    Top = 80
    object Query26sritis: TStringField
      FieldName = 'sritis'
      FixedChar = True
      Size = 12
    end
  end
  object DataSource16: TDataSource
    DataSet = Query26
    Left = 920
    Top = 80
  end
  object Query27: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'select nr, vardas, pavarde from biblio.autorius')
    Left = 840
    Top = 136
    object Query27nr: TIntegerField
      FieldName = 'nr'
    end
    object Query27vardas: TStringField
      FieldName = 'vardas'
      FixedChar = True
      Size = 12
    end
    object Query27pavarde: TStringField
      FieldName = 'pavarde'
    end
  end
  object DataSource17: TDataSource
    DataSet = Query27
    Left = 904
    Top = 136
  end
  object Query28: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      'insert into biblio.autorius values(default, :avardas, :apavarde)')
    Left = 840
    Top = 200
    ParamData = <
      item
        DataType = ftString
        Name = 'avardas'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'apavarde'
        ParamType = ptUnknown
      end>
  end
  object Query29: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'select nr, vardas, pavarde from biblio.autorius where vardas = :' +
        'avardas and pavarde = :apavarde')
    Left = 904
    Top = 200
    ParamData = <
      item
        DataType = ftString
        Name = 'avardas'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'apavarde'
        ParamType = ptUnknown
      end>
  end
  object DataSource18: TDataSource
    DataSet = Query29
    Left = 760
    Top = 200
  end
  object Query30: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'insert into biblio.knyga values (:aisbn, :apavadinimas, :aleidyk' +
        'la, :ametai, :apuslapiai, :averte, :akalba, :asritis, :aautorius' +
        ')')
    Left = 672
    Top = 200
    ParamData = <
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'apavadinimas'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'aleidykla'
        ParamType = ptUnknown
      end
      item
        DataType = ftSmallint
        Name = 'ametai'
        ParamType = ptUnknown
      end
      item
        DataType = ftSmallint
        Name = 'apuslapiai'
        ParamType = ptUnknown
      end
      item
        DataType = ftFloat
        Name = 'averte'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'akalba'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'asritis'
        ParamType = ptUnknown
      end
      item
        DataType = ftInteger
        Name = 'aautorius'
        ParamType = ptUnknown
      end>
  end
  object Query31: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'insert into biblio.cd values (:aisbn, :apavadinimas, :adata, :av' +
        'erte, :akalba, :asritis, :aautorius)')
    Left = 608
    Top = 200
    ParamData = <
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'apavadinimas'
        ParamType = ptUnknown
      end
      item
        DataType = ftDate
        Name = 'adata'
        ParamType = ptUnknown
      end
      item
        DataType = ftFloat
        Name = 'averte'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'akalba'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'asritis'
        ParamType = ptUnknown
      end
      item
        DataType = ftInteger
        Name = 'aautorius'
        ParamType = ptUnknown
      end>
  end
  object Query32: TQuery
    DatabaseName = 'Mydb'
    SessionName = 'mysession'
    SQL.Strings = (
      'select max(nr) from biblio.egzempliorius')
    Left = 528
    Top = 200
  end
  object DataSource19: TDataSource
    DataSet = Query32
    Left = 464
    Top = 200
  end
  object Query33: TQuery
    DatabaseName = 'MyDb'
    SessionName = 'mysession'
    SQL.Strings = (
      
        'insert into biblio.egzempliorius values(:anr, :aisbn, null, null' +
        ', null)')
    Left = 368
    Top = 208
    ParamData = <
      item
        DataType = ftLargeint
        Name = 'anr'
        ParamType = ptUnknown
      end
      item
        DataType = ftString
        Name = 'aisbn'
        ParamType = ptUnknown
      end>
  end
end
