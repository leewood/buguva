/*
 * Category.cpp
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#include "Category.h"
#include <QVariant>

CCategory::CCategory(QVariantMap object, bool useEnglish)
{
    this->_useEnglish = useEnglish;
    this->Init(object);
}

void CCategory::Init(QVariantMap object)
{       
    _id = object["id"].toInt();
    _name = object["name"].toString();
}
