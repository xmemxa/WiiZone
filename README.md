# WiiZone

WiiZone to wewnętrzna aplikacja napisana w technologii **Blazor Server** dla pracownika wypożyczalni gier na konsolę Nintendo Wii. Umożliwia ona wygodne zarządzanie grami, klientami oraz rezerwacjami.

## Opis funkcjonalności

- Przeglądanie listy gier, klientów i rezerwacji
- Dodawanie, edytowanie i usuwanie danych (CRUD)
- Rejestrowanie zwrotu gry
- Przegląd rezerwacji (wszystkie / aktywne / zaległe / zwrócone)
- Filtrowanie i wyszukiwanie po różnych kryteriach
- Możliwość przełączania między różnymi bazami danych w czasie działania aplikacji
- Obsługa walidacji danych i przypadków brzegowych

## Struktura bazy danych

Aplikacja korzysta z relacyjnej bazy danych, która zawiera następujące tabele:

### Games

Przechowuje dane o grach dostępnych w wypożyczalni.

| Kolumna     | Typ      | Opis                                 |
| ----------- | -------- | ------------------------------------ |
| GameId      | int (PK) | Identyfikator gry                    |
| Title       | string   | Tytuł gry                            |
| CoverImage  | string   | URL do okładki gry                   |
| Genre       | string   | Gatunek gry (np. Racing, Sports)     |
| ReleaseYear | int      | Rok wydania gry                      |
| Price       | decimal  | Cena wypożyczenia                    |
| IsReserved  | bool     | Czy aktualnie gra jest zarezerwowana |
| TimesRented | int      | Liczba wypożyczeń gry                |

---

### Clients

Przechowuje dane klientów wypożyczalni.

| Kolumna  | Typ      | Opis                    |
| -------- | -------- | ----------------------- |
| ClientId | int (PK) | Identyfikator klienta   |
| Name     | string   | Imię i nazwisko klienta |
| Email    | string   | Adres e-mail klienta    |
| Phone    | string   | Numer telefonu klienta  |

---

### Tags

Przechowuje tagi (kategorie), które można przypisać do gier.

| Kolumna | Typ      | Opis                   |
| ------- | -------- | ---------------------- |
| TagId   | int (PK) | Identyfikator tagu     |
| Name    | string   | Nazwa tagu (np. Party) |

---

### GameTags

Tabela pośrednia reprezentująca relację wiele-do-wielu między Games i Tags.

| Kolumna | Typ      | Opis    |
| ------- | -------- | ------- |
| GameId  | int (FK) | Id gry  |
| TagId   | int (FK) | Id tagu |

---

### Reservations

Przechowuje informacje o rezerwacjach gier dokonanych przez klientów.

| Kolumna        | Typ      | Opis                         |
| -------------- | -------- | ---------------------------- |
| ReservationId` | int (PK) | Identyfikator rezerwacji     |
| GameId         | int (FK) | Id gry                       |
| ClientId       | int (FK) | Id klienta                   |
| StartDate      | DateTime | Data rozpoczęcia rezerwacji  |
| EndDate        | DateTime | Data zakończenia rezerwacji  |
| IsReturned     | bool     | Czy gra została już zwrócona |

## Konfiguracja i uruchomienie

1. **Sklonuj repozytorium:**

   ```bash
   git clone https://github.com/xmemxa/WiiZone.git
   ```

2. **Ustaw połączenia do baz danych** w `appsettings.json` (sekcja `ConnectionStrings`).

3. **Uruchom projekt w trybie Blazor Server** z poziomu Ridera lub Visual Studio.

4. Jeśli baza jest pusta, aplikacja wygeneruje ją automatycznie na podstawie migracji.

## Przypadki brzegowe

- Nie można usunąć klienta z aktywną rezerwacją → pojawia się komunikat błędu
- Nie można usunąć gry, która jest aktualnie wypożyczona
- Walidacja formularzy – np. numer telefonu musi mieć konkretną długość, wymagane pola itp.
- Tytuł gry, numer telefonu i email muszą być unikalne
- Obsługa pustych baz danych

## Dynamiczna zmiana bazy danych

W ustawieniach (`/settings`) użytkownik może wybrać inną bazę danych z listy dostępnych połączeń. Aplikacja przełącza kontekst bazy w czasie działania, bez restartowania serwera.
