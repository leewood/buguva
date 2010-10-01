/*
 * Product.h
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#ifndef PRODUCT_H_
#define PRODUCT_H_

#include "Conservant.h"
#include "Category.h"
#include "MultiLanguageStrings.h"

typedef CConservant* PCConservant; 

class CProduct
{
  public:
    
    explicit CProduct(QVariantMap object, bool useEnglish);
    void Init(QVariantMap object);
    
  public:
    inline bool UsingEnglishVersion() { return _useEnglish; }
    inline bool IsApproved() { return _approved; }
    inline bool IsContentApproved() { return _approvedContent; }
    inline QString GetCalories() const { return _calories; }
    inline QString GetCompany() const { return _company; }
    inline QString GetConservantsText() const { return _conservantsText; }
    inline bool IsGMO() { return _gmo; }
    inline QString GetHazard() const { return _hazard; }
    inline int GetID() { return _id; }
    inline QString GetName() const { return _name; }
    inline QString GetPhotoUrl() const { return _photo; }
    inline QString GetTags() const { return _tags; }
    inline CCategory GetCategory() { return _category; }
    inline int GetConservantsCount() { return _conservantsCount; }
    inline CConservant& GetConservant(int i) { return _conservants[i]; }
    inline int GetHazardCode()
    {        
        if (_hazard.compare("0") == 0)
        {
            return 0;
        }
        else if (_hazard.compare("1") == 0)
        {
            return 1;
        }
        else if (_hazard.compare("2") == 0)
        {
            return 2;
        }
        else if (_hazard.compare("3") == 0)
        {
            return 3;
        }
        else if (_hazard.compare("4") == 0)
        {
            return 4;
        }
        else if (_hazard.compare("5") == 0)
        {
            return 5;
        }
        else if (_hazard.compare("6") == 0)
        {
            return 6;
        }
        else if (_hazard.compare("00") == 0)
        {
            return -1;
        }
        return -2;
    }

    static inline QString HazardMessage(int hazardCode)
    {
        switch (hazardCode)
        {
            case 0: return STR_HAZARD_0;
            case 1: return STR_HAZARD_1;
            case 2: return STR_HAZARD_2;
            case 3: return STR_HAZARD_3;
            case 4: return "";
            case -1: return STR_HAZARD_00;            
        }
        return STR_NOT_FOUND;
    }

    
    inline QString GetHazardMessage()
    {
        return HazardMessage(GetHazardCode());
    }
    
    inline QString GetHazardImage()
    {
        switch (GetHazardCode())
        {
            case 0: return "hazard0.bmp";
            case 1: return "hazard1.bmp";
            case 2: return "hazard2.bmp";
            case 3: return "hazard3.bmp";
            case 4: return "hazard4.bmp";
            case -1: return "hazard00.bmp";
        }
        return "hazard00.bmp";
    }
    
    inline bool IsVegan()
    {
        int count = GetConservantsCount();
        if (count < 1) return false;
        for (int i = 0; i < count; i++)
        {
            if (_conservants[i].IsVegan()) return true;
        }
        return false;
    }
    
    //inline CConservant*[] GetConservants() { return _conservants; }
    ~CProduct();
  private:
    bool _useEnglish;
    bool _approved;
    bool _approvedContent;
    QString _calories;
    QString _company;
    QString _conservantsText;
    bool _gmo;
    QString _hazard;
    int _id;
    QString _name;
    QString _photo;
    QString _tags;
    CCategory _category;
    QList<CConservant> _conservants;
    int _conservantsCount;
}; 

#endif /* PRODUCT_H_ */
