# DDDSample

分層架構說明

## Applications 應用層
- Commands：增刪改相關命令
- DTOs：資料轉換物件
- Mappers：物件對應設定
- Queries：讀取相關查詢

## Controllers 介面層

## Domain 領域知識層
- Entities：核心物件定義
- Repositories：核心方法定義
- Services：核心服務定義

## Infrasture 資料存取層
- Repositories：DB 實作
- Services：其他服務實作

## Middlewares 中介程式
