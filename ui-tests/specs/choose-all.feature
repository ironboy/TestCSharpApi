Feature: View all products

  Scenario: Check that the "Alla"-category shows the right products.
    Given that I am on the product page
    When I choose the category "Alla"
    Then I should see the product "Champagne"
    And I should see the product "Rysk kaviar"
    And I should see the product "Röd vin"
    And I should see the product "Basic tomatsås"
    And I should see the product "Mjöliga makaroner"
    And I should see the product "Oliver"
    And I should see the product "Potatis"
    And I should see the product "Gul lök"
    And I should see the product "Ris"
    And I should see the product "Morot"
    And I should see the product "Tomat"
    And I should see the product "Aubergine"
    And I should see the product "Äpple"
    And I should see the product "Kiwi"
    And I should see the product "Jordgubbar"