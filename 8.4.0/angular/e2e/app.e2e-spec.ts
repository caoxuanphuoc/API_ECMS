import { ECMSTemplatePage } from './app.po';

describe('ECMS App', function() {
  let page: ECMSTemplatePage;

  beforeEach(() => {
    page = new ECMSTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
