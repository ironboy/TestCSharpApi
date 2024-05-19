Feature: As user I want to be able to see the correct products listed when I have chosen a category so that I can easily filter the product list by category.

  Scenario: Check that the when I choose a "Alla" category, I see all the products with correct price.
    Given that I am on the product page
    When I choose the category "Alla"
    Then I should see the price "45" for the product "Basmatiris"
    And I should see the price "20" for the product "Potatis"
    And I should see the price "25" for the product "Pasta"
    And I should see the price "55" for the product "Fläskkarré"
    And I should see the price "115" for the product "Revbensspjäll"
    And I should see the price "140" for the product "Ryggbiff"
    And I should see the price "25" for the product "Chokladcookies"
    And I should see the price "35" for the product "Paprikachips"
    And I should see the price "45" for the product "Saltchips"
    And I should see the price "50" for the product "Jordnötssmör"
    And I should see the price "25" for the product "Vitlökssås"
    And I should see the price "15" for the product "Äpplen"
    And I should see the price "25" for the product "Bananer"
    And I should see the price "45" for the product "Citroner"
    And I should see the price "30" for the product "Tzatsikisås"

  Scenario: Make sure the right price matches product in "Kolhydrat" category
    Given that I am on the product page
    When I choose the category "Kolhydrat"
    Then I should see the price "45" for the product "Basmatiris"
    And I should see the price "20" for the product "Potatis"
    And I should see the price "25" for the product "Pasta"

  Scenario: Make sure the right price matches product in "Protein" category
    Given that I am on the product page
    When I choose the category "Protein"
    Then I should see the price "55" for the product "Fläskkarré"
    And I should see the price "115" for the product "Revbensspjäll"
    And I should see the price "140" for the product "Ryggbiff"

  Scenario: Make sure the right price matches product in "Gott & blandat" category
    Given that I am on the product page
    When I choose the category "Gott & blandat"
    Then I should see the price "25" for the product "Chokladcookies"
    And I should see the price "35" for the product "Paprikachips"
    And I should see the price "45" for the product "Saltchips"

  Scenario: Make sure the right price matches product in "Fettkälla" category
    Given that I am on the product page
    When I choose the category "Fettkälla"
    Then I should see the price "50" for the product "Jordnötssmör"
    And I should see the price "25" for the product "Vitlökssås"
    And I should see the price "30" for the product "Tzatsikisås"

  Scenario: Make sure the right price matches product in "Frukt" category
    Given that I am on the product page
    When I choose the category "Frukt"
    Then I should see the price "15" for the product "Äpplen"
    And I should see the price "25" for the product "Bananer"
    And I should see the price "45" for the product "Citroner"

  Scenario: Check that the when I choose a "Alla" category, I see all the products with correct price.
    Given that I am on the product page
    When I choose the category "Alla"
    Then I should see the product "Basmatiris" with the description "Ett långkornigt ris."
    And I should see the product "Potatis" with the description "Färsk potatis. Ekologiskt odlad i Bjärred."
    And I should see the product "Pasta" with the description "En vanlig pasta."
    And I should see the product "Fläskkarré" with the description "Riktigt gott kött, speciellt på grill."
    And I should see the product "Revbensspjäll" with the description "Saftiga revben. Från lokala kor."
    And I should see the product "Ryggbiff" with the description "Kött från saftiga del av kon."
    And I should see the product "Jordnötssmör" with the description "Ekologiskt jordnötssmör."
    And I should see the product "Tzatsikisås" with the description "En god grekisk sås, gott till grillat kött."
    And I should see the product "Vitlökssås" with the description "Hemmagjord vitlökssås som slår alla andra vitlökssåser."
    And I should see the product "Paprikachips" with the description "Chips från Estrella. Passar till att kolla film i hemmet."
    And I should see the product "Chokladcookies" with the description "Riktigt krispiga och smarriga kakor från Marabou."
    And I should see the product "Saltchips" with the description "Perfekt saltade chips från Pringles."
    And I should see the product "Äpplen" with the description "Goda, färska, Granny Smith äpplen."
    And I should see the product "Bananer" with the description "Bananer från Nicaragua."
    And I should see the product "Citroner" with the description "Goda, mogna Amalfis citroner. Odlade i Indien."