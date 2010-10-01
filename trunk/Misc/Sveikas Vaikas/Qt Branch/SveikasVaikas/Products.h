/*
 * Products.h
 *
 *  Created on: 2010.08.16
 *      Author: kuosis
 */

#ifndef PRODUCTS_H_
#define PRODUCTS_H_

#include "Product.h"

typedef CProduct* PCProduct; 

class CProducts
{
  public:
    explicit CProducts(QString object, bool useEnglish);
    void Init(QString object);
    
  public:
    inline bool UsingEnglishVersion() { return _useEnglish; }
    inline bool GetProductsCount() { return _count; }
    inline CProduct& GetProduct(int i) { return _products[i]; }
    inline bool HasError() { return _hasError; }
    static inline QString HazardMessage(int hazardCode) { return CProduct::HazardMessage(hazardCode); }
    inline int GetHazardCode()
    {
        if (HasError() || (GetProductsCount() == 0))
        {
            return -2;
        }
        else
        {
            return GetProduct(0).GetHazardCode();
        }
    }
    inline bool IsVegan()
    {
        if (HasError() || (GetProductsCount() == 0))
        {
            return false;
        }
        return GetProduct(0).IsVegan();
    }
    inline bool IsGMO()
    {
        if (HasError() || (GetProductsCount() == 0))
        {
            return false;
        }
        return GetProduct(0).IsGMO();
    }
    
    inline QString GetHazardMessage()
    {
        return HazardMessage(GetHazardCode());
    }

    ~CProducts();
  private:
    bool _useEnglish;
    int _count;
    bool _hasError;
    QList<CProduct> _products;
    
};  

#endif /* PRODUCTS_H_ */
