import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

Given('that I am on the product page', () => {
  cy.visit('/products');
});

When('I choose the category {string}', (category) => {
  cy.get('#categories').select(category);
});

Then('I should see the product {string}', (productName) => {
  cy.get('.product .name').contains(productName);
});

Then('I should see the price {string}', (productPrice) => {
  cy.get('.price.name').contains(productPrice);
});

Then('I should see the price {string} for the product {string}', (productPrice, productName) => {
  // Implement verifying product name and price using Cypress
  cy.contains('.product .name', productName).siblings('.price').should('contain', productPrice);
});

Then('I should see the product {string} with the description {string}', (productName, productDescription => {
  cy.contains('.producnt .name', productName).siblings('.price').should('contain', productDescription);
}));