import { Given, When, Then } from "@badeball/cypress-cucumber-preprocessor";

const allDescriptions = {
  Alla: ['En enkel men god tomatsås', 'Den finaste kaviaren, fast inte från Ryssland egentligen.', 'Ganska mjöliga men energirika makaroner', 'Husets egen blandning.', 'Fina knölar', 'En sak som garanterat får dig att gråta.','Goda oliver.', 'Det finaste och dyraste i Sverige', 'Alltid najs med ris.', 'En orange rotfrukt.', 'En mångsidig grönsak som används till allt.', 'En lila grönsak som är populär i många rätter.', 'En mångsidig frukt som finns i olika sorter och smaker.', 'Grön frukt med hög C-Vitamininnehåll.', 'Söta,röda bär som är populära som sommarfrukt.'],
  Lyx: ['Den finaste kaviaren, fast inte från Ryssland egentligen.', 'Husets egen blandning.', 'Det finaste och dyraste i Sverige'],
  Prisvänligt: ['En enkel men god tomatsås', 'Ganska mjöliga men energirika makaroner', 'Goda oliver.'],
  Vardag: ['Fina knölar', 'En sak som garanterat får dig att gråta.', 'Alltid najs med ris.'],
  Grönsaker: ['En orange rotfrukt.', 'En mångsidig grönsak som används till allt.', 'En lila grönsak som är populär i många rätter.'],
  Frukt: ['En mångsidig frukt som finns i olika sorter och smaker.', 'Grön frukt med hög C-Vitamininnehåll.', 'Söta,röda bär som är populära som sommarfrukt.']
};

Given('att jag besöker produktsidan', () => {
  cy.visit('/products');
});

When('jag väljer {string} i kategoriväljaren', (category) => {
  cy.get('#categories').select(category);
});

Then('ska rätt beskrivning visas för varje produkt', () => {
  const expectedDescriptions = allDescriptions['Alla'];

  cy.get('.product .description').should('be.visible');

  cy.get('.product').each(($el, index) => {
    cy.wrap($el).find('.name').invoke('text').then((productName) => {
      const descriptionText = expectedDescriptions[index].trim();
      cy.wrap($el).find('.description').invoke('text').should('contain', descriptionText);
    });
  });
});

Then('ska rätt beskrivning visas för varje produkt i kategorin {string}', (category) => {
  const expectedDescriptions = allDescriptions[category];
  
  cy.wait(2000);
  
  cy.get('.product').each(($el, index) => {
    const descriptionText = expectedDescriptions[index].trim();
    cy.wrap($el).find('.description', { timeout: 10000 }).should('be.visible').and('contain', descriptionText);
  });
});