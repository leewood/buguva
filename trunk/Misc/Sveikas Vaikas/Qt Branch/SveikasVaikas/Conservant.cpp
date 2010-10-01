#include "Conservant.h"
#include <QVariant>
CConservant::CConservant(QVariantMap object, bool useEnglish)
{
    this->_useEnglish = useEnglish;
    this->Init(object);
}

void CConservant::Init(QVariantMap object)
{   
   _category = object["category"].toInt();
   _diseases = object["diseases"].toString();
   _id = object["id"].toInt();
   if (UsingEnglishVersion())
   {
       _name = object["nameEnglish"].toString();
   }
   else
   {
       _name = object["name"].toString();
   }
   _number = object["number"].toString();
   _vegan = object["vegan"].toBool();
   _bannedUSA = object["bannedInUsa"].toBool();
   _bannedAustralia = object["bannedInAustralia"].toBool();
   _bannedCanada = object["bannedInCanada"].toBool();
}
