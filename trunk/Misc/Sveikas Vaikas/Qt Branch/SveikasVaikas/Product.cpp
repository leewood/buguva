/*
 * Product.cpp
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#include "Product.h"


CProduct::CProduct(QVariantMap object, bool useEnglish)
  {
    this->_useEnglish = useEnglish;
    this->Init(object);
  }

void CProduct::Init(QVariantMap object)
{      
   _id = object["id"].toInt();
   _name = object["name"].toString();
   _approved = object["approved"].toBool();
   _approvedContent = object["approvedContent"].toBool();
   _company = object["company"].toString();
   _calories = object["calories"].toString();
   _conservantsText = object["conservantsText"].toString();
   _gmo = object["gmo"].toBool();
   _hazard = object["hazard"].toString();
   _photo = object["photo"].toString();
   _tags = object["tags"].toString();
   CCategory temp(object["category"].toMap(), UsingEnglishVersion());
   _category = temp;
   _conservants.clear();
   foreach (QVariant conserv, object["conservants"].toList())
   {
       _conservants.append(*(new CConservant(conserv.toMap(), UsingEnglishVersion())));
   }

   _conservantsCount = _conservants.count();
}

CProduct::~CProduct()
{
    //delete _category;
    _conservants.clear();        
}
