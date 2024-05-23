/*
import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('att jag besöker startsidan', () => {
  cy.visit('/products');
});

When('jag väljer kategorin {string}', (category) => {
  cy.get('#categories').select(category);
});

Then('ska jag se rätt produkter för kategorin {string}', (category) => {
  // Kod för att verifiera att rätt produkter visas för den valda kategorin
  cy.get('.product').each(($product) => {
    // Hämta klassattributet för varje produkt och dela upp det i en array av klasser
    cy.wrap($product).invoke('attr', 'class').then((productClass) => {
      // Kontrollera om produktens klass innehåller den förväntade kategorin
      expect(productClass.includes(category.toLowerCase())).to.be.false;
    });
  });
});
*/