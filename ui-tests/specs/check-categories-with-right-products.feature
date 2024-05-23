Feature: Kontrollera att rätt produkter visas för vald kategori

  Scenario: Visa produkter i kategorin "Lyx"
    Given att jag besöker startsidan
    When jag väljer kategorin "Lyx"
    Then ska jag se rätt produkter för kategorin "Lyx"

  Scenario: Visa produkter i kategorin "Prisvänligt"
    Given att jag besöker startsidan
    When jag väljer kategorin "Prisvänligt"
    Then ska jag se rätt produkter för kategorin "Prisvänligt"

  Scenario: Visa produkter i kategorin "Vardag"
    Given att jag besöker startsidan
    When jag väljer kategorin "Vardag"
    Then ska jag se rätt produkter för kategorin "Vardag"

  Scenario: Visa produkter i kategorin "Grönsaker"
    Given att jag besöker startsidan
    When jag väljer kategorin "Grönsaker"
    Then ska jag se rätt produkter för kategorin "Grönsaker"

  Scenario: Visa produkter i kategorin "Frukt"
    Given att jag besöker startsidan
    When jag väljer kategorin "Frukt"
    Then ska jag se rätt produkter för kategorin "Frukt"