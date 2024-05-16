Feature: As user I want to be able to see the correct products listed when I have chosen a category so that I can easily filter the product list by category.

  Scenario Outline: Check that the category <category> show the product <product>.
    Given that I am on the product page
    When I choose the category "<category>"
    Then I should see the product "<product>"

    Examples:
      | category    | product           |
      | Prisvänligt | Basic tomatsås    |
      | Prisvänligt | Mjöliga makaroner |
      | Vardag      | Potatis           |
      | Vardag      | Gul lök           |
      | Lyx         | Champagne         |
      | Lyx         | Rysk kaviar       |

  Scenario Outline: Check that the category <category> does not show the product <product>.
    Given that I am on the product page
    When I choose the category "<category>"
    Then I should not see the product "<product>"

    Examples:
      | category    | product           |
      | Prisvänligt | Basic tomatsås    |
      | Vardag      | Mjöliga makaroner |
      | Lyx         | Potatis           |
      | Lyx         | Gul lök           |
      | Prisvänligt | Champagne         |
      | Prisvänligt | Rysk kaviar       |