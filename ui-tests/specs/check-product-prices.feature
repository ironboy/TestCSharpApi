Feature: Kontrollera produktpriser på produktsidan

  Scenario: Rätt pris visas för varje produkt när "Alla" är valt
    Given att jag besöker produktsidan
    When jag väljer "Alla" i kategoriväljaren
    Then ska rätt pris visas för varje produkt

  Scenario: Rätt pris visas för varje produkt när "Lyx" är valt
    Given att jag besöker produktsidan
    When jag väljer kategorin "Lyx"
    Then ska rätt pris visas för varje produkt i kategorin "Lyx"

  Scenario: Rätt pris visas för varje produkt när "Prisvänligt" är valt
    Given att jag besöker produktsidan
    When jag väljer kategorin "Prisvänligt"
    Then ska rätt pris visas för varje produkt i kategorin "Prisvänligt"

  Scenario: Rätt pris visas för varje produkt när "Vardag" är valt
    Given att jag besöker produktsidan
    When jag väljer kategorin "Vardag"
    Then ska rätt pris visas för varje produkt i kategorin "Vardag"

  Scenario: Rätt pris visas för varje produkt när "Grönsaker" är valt
    Given att jag besöker produktsidan
    When jag väljer kategorin "Grönsaker"
    Then ska rätt pris visas för varje produkt i kategorin "Grönsaker"

  Scenario: Rätt pris visas för varje produkt när "Frukt" är valt
    Given att jag besöker produktsidan
    When jag väljer kategorin "Frukt"
    Then ska rätt pris visas för varje produkt i kategorin "Frukt"