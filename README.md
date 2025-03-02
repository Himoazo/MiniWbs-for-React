# Mini WMS 

Denna applikation är en REST API för ett minimalt lagerhanteringssystem.

API:et är utvecklad med .NET 9 och använder SQLite som databas. 

Detta API är publicerat [HÄR](https://miniwbs-for-react-production.up.railway.app/) och konsumeras av en React applikation som är publicerad [HÄR](https://react-wms.netlify.app/)

## Instruktionerna för körning av applikation i lokalmiljö:
1.	Klona repo:t 
```bash
git clone https://github.com/Himoazo/MiniWbs-for-React.git
```

2.	Gå till appsettings.json:
```json
"JWT": {
    "Issuer": "",
    "Audience": "",
    "Signingkey": ""
  }
  ```

3. Fyll i de tomma parametrarna ovan med följande data:
```json
"JWT": {
    "Issuer": "http://localhost:5246",
    "Audience": "http://localhost:5246",
    "Signingkey": "caf8f16ef80a821f4dbd6706fa6f29e79b16df00f5def798c8f897f2e7a44c8e5b52a3f7a6f34d2624a3f75a60d8611bb01c18f04eeafc9da835cc5a1c00db0f"
  }
  ```
*Obs.* Issuer och Audience värde ska matcha med sökvägen i den lokala miljön om porten 5258 är upptagen då ska 5258 ändras till den aktuella port som applikationen körs på.
*Obs.* Signingkey måste uppfylla speciella kriterier nyckeln ovan uppfyller dessa krav men en annan nyckel kan också användas.

4.	Build och run. 


## API Endpoints

POST `/api/account/login`
POST `/api/account/register`
GET `/api/account/me`
GET `/api/Products`
POST `/api/Products`
GET `/api/Products/{id}`
PUT `/api/Products/{id}`
DELETE `/api/Products/{id}`