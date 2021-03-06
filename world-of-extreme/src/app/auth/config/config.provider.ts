import { Injectable } from '@angular/core';
import { PlatformProvider } from '../utils/platform-provider/platform.provider';
import { DEFAULT_CONFIG } from './default-config';
import { OpenIdConfiguration } from './openid-configuration';

@Injectable()
export class ConfigurationProvider {
  private openIdConfigurationInternal: OpenIdConfiguration;

  constructor(private platformProvider: PlatformProvider) {}

  hasValidConfig() {
    return !!this.openIdConfigurationInternal;
  }

  getOpenIDConfiguration(): OpenIdConfiguration {
    return this.openIdConfigurationInternal || null;
  }

  setConfig(configuration: OpenIdConfiguration) {
    this.openIdConfigurationInternal = { ...DEFAULT_CONFIG, ...configuration };

    if (configuration?.storage) {
      console.warn(
        `PLEASE NOTE: The storage in the config will be deprecated in future versions:
                Please pass the custom storage in forRoot() as documented`
      );
    }

    this.setSpecialCases(this.openIdConfigurationInternal);

    return this.openIdConfigurationInternal;
  }

  private setSpecialCases(currentConfig: OpenIdConfiguration) {
    if (!this.platformProvider.isBrowser) {
      currentConfig.startCheckSession = false;
      currentConfig.silentRenew = false;
      currentConfig.useRefreshToken = false;
      currentConfig.usePushedAuthorisationRequests = false;
    }
  }
}
