/*
 * Products.cpp
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#include "Products.h"
#include "parser.h"

CProducts::CProducts(QString object, bool useEnglish)
{
  this->_useEnglish = useEnglish;
  this->Init(object);
}

void CProducts::Init(QString object)
{
    _count = 0;
    _hasError = false;
    if (object.compare("ERROR") == 0)
    {        
        _hasError = true;
        return;
    }
    QJson::Parser parser;
    bool ok;
    QVariant result = parser.parse(object.toAscii(), &ok);
    if (!ok)
    {
        _hasError = true;
        return;
    }
    _products.clear();
    if (result.canConvert(result.Map))
    {
        CProduct product(result.toMap(), this->UsingEnglishVersion());
        _products.append(product);
    }
    else
    {
        foreach (QVariant item, result.toList())
        {
            CProduct product(item.toMap(), this->UsingEnglishVersion());
            _products.append(product);
        }
    }
    
}

CProducts::~CProducts()
{
   _products.clear();
}
