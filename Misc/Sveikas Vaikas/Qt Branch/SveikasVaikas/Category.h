/*
 * Category.h
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#ifndef CATEGORY_H_
#define CATEGORY_H_
#include <QVariant>

class CCategory
{
  public:
    explicit CCategory() {}
    explicit CCategory(QVariantMap object, bool useEnglish);
    void Init(QVariantMap object);
    
  public:
    inline bool UsingEnglishVersion() { return _useEnglish; }
    inline int GetID() { return _id; }
    inline QString GetName() { return _name; }
    
  private:
    bool _useEnglish;
    int _id;
    QString _name;
    
};    

#endif /* CATEGORY_H_ */
