/*
import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

const allPrices = {
  Alla: ['10', '500', '15', '999', '20', '30', '10', '999', '25', '10', '30', '20', '15', '20', '40'],
  Lyx: ['500', '999', '999'],
  Prisvänligt: ['10', '15', '10'],
  Vardag: ['20', '30', '25'],
  Grönsaker: ['10', '30', '20'],
  Frukt: ['15', '20', '40']
};

Given('att jag besöker produktsidan', () => {
  cy.visit('/products');
});

When('jag väljer {string} i kategoriväljaren', (category) => {
  cy.get('#categories').select(category);
});

When('jag väljer kategorin {string}', (category) => {
  cy.get('#categories').select(category);
});

Then('ska rätt pris visas för varje produkt', () => {
  const expectedPrices = allPrices['Alla'];

  cy.get('.product .price').should('be.visible');

  cy.get('.product').each(($el, index) => {
    cy.wrap($el).find('.name').invoke('text').then((productName) => {
      
      const match = productName.match(/\d+/);
      const priceText = match ? match[0] : '';

      const expectedPrice = expectedPrices[index].trim();
    });
  });
});

Then('ska rätt pris visas för varje produkt i kategorin {string}', (category) => {
  const expectedPrices = allPrices[category];
  
  cy.wait(2000);
  
  cy.get('.product').each(($el, index) => {
    cy.wrap($el).find('.price', { timeout: 10000 }).should('be.visible').and('contain', expectedPrices[index]);
  });
});
*/