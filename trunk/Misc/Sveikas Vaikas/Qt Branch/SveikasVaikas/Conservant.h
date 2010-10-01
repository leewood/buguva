/*
 * Conservant.h
 *
 *  Created on: 2010.08.13
 *      Author: kuosis
 */

#ifndef CONSERVANT_H_
#define CONSERVANT_H_
 #include <QVariant>

class CConservant
{
  public:
    explicit CConservant(QVariantMap object, bool useEnglish);
    void Init(QVariantMap object);
    
    
  public:
    inline bool UsingEnglishVersion() { return _useEnglish; }
    inline int GetCategory() { return _category; }
    inline QString GetDiseases() { return _diseases; }
    inline int GetID() { return _id; }
    inline QString GetName() { return _name; }
    inline QString GetNumber() { return _number; }
    inline bool IsBannedInUSA() { return _bannedUSA; }
    inline bool IsBannedInCanada() { return _bannedCanada; }
    inline bool IsBannedInAustralia() { return _bannedAustralia; }
    inline bool IsVegan() { return _vegan; }
    
  private:
    bool _useEnglish;
    int _category;
    QString _diseases;
    int _id;
    QString _name;
    QString _number;
    bool _bannedUSA;
    bool _bannedCanada;
    bool _bannedAustralia;
    bool _vegan;
};

#endif /* CONSERVANT_H_ */
