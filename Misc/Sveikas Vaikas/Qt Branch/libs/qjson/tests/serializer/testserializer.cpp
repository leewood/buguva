/* This file is part of QJson
 *
 * Copyright (C) 2009 Flavio Castelli <flavio.castelli@gmail.com>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License as published by the Free Software Foundation; either
 * version 2 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public License
 * along with this library; see the file COPYING.LIB.  If not, write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA 02110-1301, USA.
 */

#include <limits>

#include <QtTest/QtTest>

#include "parser.h"
#include "serializer.h"

#include <QtCore/QVariant>


class TestSerializer: public QObject
{
  Q_OBJECT
  private slots:
    void testReadWriteEmptyDocument();
    void testReadWrite();
    void testReadWrite_data();
    void testValueNull();
    void testValueString();
    void testValueString_data();
    void testValueStringList();
    void testValueStringList_data();
    void testValueInteger();
    void testValueInteger_data();
    void testValueDouble();
    void testValueDouble_data();
    void testValueFloat();
    void testValueFloat_data();
    void testValueBoolean();
    void testValueBoolean_data();
    void testSpecialNumbers();
    void testSpecialNumbers_data();

  private:
    void valueTest( const QVariant& value, const QString& expectedRegExp, bool errorExpected = false );
    void valueTest( const QObject* object, const QString& expectedRegExp );
};

Q_DECLARE_METATYPE(QVariant)

using namespace QJson;

void TestSerializer::testReadWriteEmptyDocument()
{
  QByteArray json = "";
  Parser parser;
  bool ok;
  QVariant result = parser.parse( json, &ok );
  QVERIFY(ok);
  QVERIFY( ! result.isValid() );
  Serializer serializer;
  const QByteArray serialized = serializer.serialize( result );
  QVERIFY( !serialized.isNull() );
  QByteArray expected = "null";
  QCOMPARE(expected, serialized);
}

void TestSerializer::testReadWrite()
{
  QFETCH( QByteArray, json );
  Parser parser;
  bool ok;
  QVariant result = parser.parse( json, &ok );
  QVERIFY(ok);
  Serializer serializer;
  const QByteArray serialized = serializer.serialize( result );
  QVariant writtenThenRead = parser.parse( serialized, &ok );
  QVERIFY(ok);
  QCOMPARE( result, writtenThenRead );
}

void TestSerializer::testReadWrite_data()
{
    QTest::addColumn<QByteArray>( "json" );

    // array tests
    QTest::newRow( "empty array" ) << QByteArray("[]");
    QTest::newRow( "basic array" ) << QByteArray("[\"person\",\"bar\"]");
    QTest::newRow( "single int array" ) << QByteArray("[6]");
    QTest::newRow( "int array" ) << QByteArray("[6,5,6,7]");
    const QByteArray json = "[1,2.4, -100, -3.4, -5e+, 2e,3e+,4.3E,5.4E-]";
    QTest::newRow( QByteArray("array of various numbers") ) << json;

    // document tests
    QTest::newRow( "empty object" ) << QByteArray("{}");
    QTest::newRow( "basic document" ) << QByteArray("{\"person\":\"bar\"}");
    QTest::newRow( "object with ints" ) << QByteArray("{\"person\":6}");
    const QByteArray json2 = "{ \"person\":\"bar\",\n\"number\" : 51.3 , \"array\":[\"item1\", 123]}";
    QTest::newRow( "complicated document" ) << json2;

    // more complex cases
    const QByteArray json3 = "[ {\"person\":\"bar\"},\n\"number\",51.3 , [\"item1\", 123]]";
    QTest::newRow( "complicated array" ) << json3;
}


void TestSerializer::valueTest( const QVariant& value, const QString& expectedRegExp, bool errorExpected )
{
  Serializer serializer;
  const QByteArray serialized = serializer.serialize( value );
  QCOMPARE(serialized.isNull(), errorExpected);
  const QString serializedUnicode = QString::fromUtf8( serialized );
  if (!errorExpected) {
    QRegExp expected( expectedRegExp );
    QVERIFY( expected.isValid() );
    QVERIFY2( expected.exactMatch( serializedUnicode ),
      qPrintable( QString( QLatin1String( "Expected regexp \"%1\" but got \"%2\"." ) )
        .arg( expectedRegExp ).arg( serializedUnicode ) ) );
  }
}

void TestSerializer::valueTest( const QObject* object, const QString& expectedRegExp )
{
  Serializer serializer;
  const QByteArray serialized = serializer.serialize( object );
  const QString serializedUnicode = QString::fromUtf8( serialized );
  QRegExp expected( expectedRegExp );
  QVERIFY( expected.isValid() );
  QVERIFY2( expected.exactMatch( serializedUnicode ),
    qPrintable( QString( QLatin1String( "Expected regexp \"%1\" but got \"%2\"." ) )
      .arg( expectedRegExp ).arg( serializedUnicode ) ) );
}

void TestSerializer::testValueNull()
{
  valueTest( QVariant(), QLatin1String( "\\s*null\\s*" ) );
  QVariantMap map;
  map[QLatin1String("value")] = QVariant();
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:\\s*null\\s*\\}\\s*" ) );
}

void TestSerializer::testValueString()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  valueTest( value, expected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ) );
}

void TestSerializer::testValueString_data()
{
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );
  QTest::newRow( "null string" ) << QVariant( QString() ) << QString( QLatin1String( "\\s*\"\"\\s*" ) );
  QTest::newRow( "empty string" ) << QVariant( QString( QLatin1String( "" ) ) ) << QString( QLatin1String( "\\s*\"\"\\s*" ) );
  QTest::newRow( "Simple String" ) << QVariant( QString( QLatin1String( "simpleString" ) ) ) << QString( QLatin1String( "\\s*\"simpleString\"\\s*" ) );
  QTest::newRow( "string with tab" ) << QVariant( QString( QLatin1String( "string\tstring" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\tstring\"\\s*" ) );
  QTest::newRow( "string with newline" ) << QVariant( QString( QLatin1String( "string\nstring" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\nstring\"\\s*" ) );
  QTest::newRow( "string with bell" ) << QVariant( QString( QLatin1String( "string\bstring" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\bstring\"\\s*" ) );
  QTest::newRow( "string with return" ) << QVariant( QString( QLatin1String( "string\rstring" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\rstring\"\\s*" ) );
  QTest::newRow( "string with double quote" ) << QVariant( QString( QLatin1String( "string\"string" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\\"string\"\\s*" ) );
  QTest::newRow( "string with backslash" ) << QVariant( QString( QLatin1String( "string\\string" ) ) ) << QString( QLatin1String( "\\s*\"string\\\\\\\\string\"\\s*" ) );
  QString testStringWithUnicode = QString( QLatin1String( "string" ) ) + QChar( 0x2665 ) + QLatin1String( "string" );
  QString testEscapedString = QString( QLatin1String( "string" ) ) + QLatin1String("\\\\u2665") + QLatin1String( "string" );
  QTest::newRow( "string with unicode" ) << QVariant( testStringWithUnicode ) << QLatin1String( "\\s*\"" ) + testEscapedString + QLatin1String( "\"\\s*" );
}

void TestSerializer::testValueStringList()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  valueTest( value, expected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ) );
}

void TestSerializer::testValueStringList_data()
{
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );

  QStringList stringlist;
  QString expected;

  // simple QStringList
  stringlist << QLatin1String("hello") << QLatin1String("world");
  expected = QLatin1String( "\\s*\\[\\s*\"hello\"\\s*,\\s*\"world\"\\s*\\]\\s*" );
  QTest::newRow( "simple QStringList" ) << QVariant( stringlist) << expected;
}

void TestSerializer::testValueInteger()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  valueTest( value, expected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ) );
}

void TestSerializer::testValueInteger_data()
{
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );

  QTest::newRow( "int 0" ) << QVariant( static_cast<int>( 0 ) ) << QString( QLatin1String( "\\s*0\\s*" ) );
  QTest::newRow( "uint 0" ) << QVariant( static_cast<uint>( 0 ) ) << QString( QLatin1String( "\\s*0\\s*" ) );
  QTest::newRow( "int -1" ) << QVariant( static_cast<int>( -1 ) ) << QString( QLatin1String( "\\s*-1\\s*" ) );
  QTest::newRow( "int 2133149800" ) << QVariant( static_cast<int>(2133149800) ) << QString( QLatin1String( "\\s*2133149800\\s*" ) );
  QTest::newRow( "uint 4133149800" ) << QVariant( static_cast<uint>(4133149800u) ) << QString( QLatin1String( "\\s*4133149800\\s*" ) );
  QTest::newRow( "uint64 932838457459459" ) << QVariant( Q_UINT64_C(932838457459459) ) << QString( QLatin1String( "\\s*932838457459459\\s*" ) );
  QTest::newRow( "max unsigned long long" ) << QVariant( std::numeric_limits<unsigned long long>::max() ) << QString( QLatin1String( "\\s*%1\\s*" ) ).arg(std::numeric_limits<unsigned long long>::max());
}

void TestSerializer::testValueDouble()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  QFETCH( bool, errorExpected );
  valueTest( value, expected, errorExpected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ), errorExpected );
}

void TestSerializer::testValueDouble_data()
{
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );
  QTest::addColumn<bool>( "errorExpected" );

  QTest::newRow( "double 0" ) << QVariant( 0.0 ) << QString( QLatin1String( "\\s*0.0\\s*" ) ) << false;
  QTest::newRow( "double -1" ) << QVariant( -1.0 ) << QString( QLatin1String( "\\s*-1.0\\s*" ) ) << false;
  QTest::newRow( "double 1.5E-20" ) << QVariant( 1.5e-20 ) << QString( QLatin1String( "\\s*1.5[Ee]-20\\s*" ) ) << false;
  QTest::newRow( "double -1.5E-20" ) << QVariant( -1.5e-20 ) << QString( QLatin1String( "\\s*-1.5[Ee]-20\\s*" ) ) << false;
  QTest::newRow( "double 2.0E-20" ) << QVariant( 2.0e-20 ) << QString( QLatin1String( "\\s*2(?:.0)?[Ee]-20\\s*" ) ) << false;
  QTest::newRow( "double infinity" ) << QVariant( std::numeric_limits< double >::infinity() ) << QString( ) << true;
  QTest::newRow( "double -infinity" ) << QVariant( -std::numeric_limits< double >::infinity() ) << QString( ) << true;
  QTest::newRow( "double NaN" ) << QVariant( std::numeric_limits< double >::quiet_NaN() ) << QString( ) << true;
}

void TestSerializer::testValueFloat()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  QFETCH( bool, errorExpected );
  valueTest( value, expected, errorExpected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ), errorExpected );
}

void TestSerializer::testValueFloat_data()
{
  QVariant v (QMetaType::Float);
  float value;

  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );
  QTest::addColumn<bool>( "errorExpected" );

  value = 0;
  v.setValue(value);
  QTest::newRow( "float 0" ) << v << QString( QLatin1String( "\\s*0.0\\s*" ) ) << false;

  value = -1;
  v.setValue(value);
  QTest::newRow( "float -1" ) << v << QString( QLatin1String( "\\s*-1.0\\s*" ) ) << false;

  value = 1.12;
  v.setValue(value);
  QTest::newRow( "float 1.12" ) << v << QString( QLatin1String( "\\s*1.12\\s*" ) ) << false;
}

void TestSerializer::testValueBoolean()
{
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  valueTest( value, expected );

  QVariantMap map;
  map[QLatin1String("value")] = value;
  valueTest( QVariant(map), QLatin1String( "\\s*\\{\\s*\"value\"\\s*:" ) + expected + QLatin1String( "\\}\\s*" ) );
}

void TestSerializer::testValueBoolean_data()
{
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );

  QTest::newRow( "bool false" ) << QVariant( false ) << QString( QLatin1String( "\\s*false\\s*" ) );
  QTest::newRow( "bool true" ) << QVariant( true ) << QString( QLatin1String( "\\s*true\\s*" ) );
}

void TestSerializer::testSpecialNumbers() {
  QFETCH( QVariant, value );
  QFETCH( QString, expected );
  Serializer specialSerializer;
  QVERIFY(!specialSerializer.specialNumbersAllowed());
  specialSerializer.allowSpecialNumbers(true);
  QVERIFY(specialSerializer.specialNumbersAllowed());
  QByteArray serialized = specialSerializer.serialize(value);
  QCOMPARE(QString::fromLocal8Bit(serialized), expected);

}

void TestSerializer::testSpecialNumbers_data() {
  QTest::addColumn<QVariant>( "value" );
  QTest::addColumn<QString>( "expected" );

  QTest::newRow( "Infinity" ) << QVariant( std::numeric_limits< double >::infinity() ) << QString::fromLocal8Bit("Infinity");
  QTest::newRow( "-Infinity" ) << QVariant( -std::numeric_limits< double >::infinity() ) << QString::fromLocal8Bit("-Infinity");
  QTest::newRow( "Infinity" ) <<  QVariant( std::numeric_limits< double >::quiet_NaN() ) << QString::fromLocal8Bit("NaN");
}

QTEST_MAIN(TestSerializer)

#ifdef QMAKE_BUILD
  #include "testserializer.moc"
#else
  #include "moc_testserializer.cxx"
#endif
