
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
#include "person.h"
#include "serializer.h"
#include "qobjecthelper.h"

#include <QtCore/QVariant>

class TestQObjectHelper: public QObject
{
  Q_OBJECT
  private slots:
    void testQObject2QVariant();
    void testQVariant2QObject();
};

using namespace QJson;

void TestQObjectHelper::testQObject2QVariant()
{
  QString name = QLatin1String("Flavio Castelli");
  int phoneNumber = 123456;
  Person::Gender gender = Person::Male;
  QDate dob (1982, 7, 12);

  Person person;
  person.setName(name);
  person.setPhoneNumber(phoneNumber);
  person.setGender(gender);
  person.setDob(dob);

  QVariantMap expected;
  expected[QLatin1String("name")] = QVariant(name);
  expected[QLatin1String("phoneNumber")] = QVariant(phoneNumber);
  expected[QLatin1String("gender")] = QVariant(gender);
  expected[QLatin1String("dob")] = QVariant(dob);

  QVariantMap result = QObjectHelper::qobject2qvariant(&person);

  QCOMPARE(result, expected);
}

void TestQObjectHelper::testQVariant2QObject()
{
  bool ok;
  QString name = QLatin1String("Flavio Castelli");
  int phoneNumber = 123456;
  Person::Gender gender = Person::Male;
  QDate dob (1982, 7, 12);

  Person expected_person;
  expected_person.setName(name);
  expected_person.setPhoneNumber(phoneNumber);
  expected_person.setGender(gender);
  expected_person.setDob(dob);

  QVariantMap variant = QObjectHelper::qobject2qvariant(&expected_person);

  Serializer serializer;
  QByteArray json = serializer.serialize(variant);

  Parser parser;
  QVariant parsedVariant = parser.parse(json,&ok);
  QVERIFY(ok);
  QVERIFY(parsedVariant.canConvert(QVariant::Map));

  Person person;
  QObjectHelper::qvariant2qobject(parsedVariant.toMap(), &person);

  QCOMPARE(person.name(), name);
  QCOMPARE(person.phoneNumber(),phoneNumber);
  QCOMPARE(person.gender(), gender);
  QCOMPARE(person.dob(), dob);
}

QTEST_MAIN(TestQObjectHelper)
#ifdef QMAKE_BUILD
  #include "testqobjecthelper.moc"
#else
  #include "moc_testqobjecthelper.cxx"
#endif
