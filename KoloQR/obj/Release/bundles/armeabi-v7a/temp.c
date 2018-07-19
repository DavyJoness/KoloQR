/* This source code was produced by mkbundle, do not edit */

#ifndef NULL
#define NULL (void *)0
#endif

typedef struct {
	const char *name;
	const unsigned char *data;
	const unsigned int size;
} MonoBundledAssembly;
void          mono_register_bundled_assemblies (const MonoBundledAssembly **assemblies);
void          mono_register_config_for_assembly (const char* assembly_name, const char* config_xml);

typedef struct _compressed_data {
	MonoBundledAssembly assembly;
	int compressed_size;
} CompressedAssembly;

extern const unsigned char assembly_data_KoloQR_dll [];
static CompressedAssembly assembly_bundle_KoloQR_dll = {{"KoloQR.dll", assembly_data_KoloQR_dll, 26112}, 12052};
extern const unsigned char assembly_data_MySql_Data_CF_dll [];
static CompressedAssembly assembly_bundle_MySql_Data_CF_dll = {{"MySql.Data.CF.dll", assembly_data_MySql_Data_CF_dll, 356864}, 143505};
extern const unsigned char assembly_data_Xamarin_Android_Support_v4_dll [];
static CompressedAssembly assembly_bundle_Xamarin_Android_Support_v4_dll = {{"Xamarin.Android.Support.v4.dll", assembly_data_Xamarin_Android_Support_v4_dll, 1930752}, 403369};
extern const unsigned char assembly_data_zxing_monoandroid_dll [];
static CompressedAssembly assembly_bundle_zxing_monoandroid_dll = {{"zxing.monoandroid.dll", assembly_data_zxing_monoandroid_dll, 442368}, 189932};
extern const unsigned char assembly_data_ZXing_Net_Mobile_dll [];
static CompressedAssembly assembly_bundle_ZXing_Net_Mobile_dll = {{"ZXing.Net.Mobile.dll", assembly_data_ZXing_Net_Mobile_dll, 37376}, 16252};
extern const unsigned char assembly_data_I18N_West_dll [];
static CompressedAssembly assembly_bundle_I18N_West_dll = {{"I18N.West.dll", assembly_data_I18N_West_dll, 71168}, 19822};
extern const unsigned char assembly_data_Mono_Android_dll [];
static CompressedAssembly assembly_bundle_Mono_Android_dll = {{"Mono.Android.dll", assembly_data_Mono_Android_dll, 20595200}, 4613838};
extern const unsigned char assembly_data_mscorlib_dll [];
static CompressedAssembly assembly_bundle_mscorlib_dll = {{"mscorlib.dll", assembly_data_mscorlib_dll, 3344896}, 1154583};
extern const unsigned char assembly_data_System_Core_dll [];
static CompressedAssembly assembly_bundle_System_Core_dll = {{"System.Core.dll", assembly_data_System_Core_dll, 862208}, 294795};
extern const unsigned char assembly_data_System_Data_dll [];
static CompressedAssembly assembly_bundle_System_Data_dll = {{"System.Data.dll", assembly_data_System_Data_dll, 972288}, 320243};
extern const unsigned char assembly_data_System_dll [];
static CompressedAssembly assembly_bundle_System_dll = {{"System.dll", assembly_data_System_dll, 1651200}, 594742};
extern const unsigned char assembly_data_System_Windows_dll [];
static CompressedAssembly assembly_bundle_System_Windows_dll = {{"System.Windows.dll", assembly_data_System_Windows_dll, 10752}, 3028};
extern const unsigned char assembly_data_System_Xml_dll [];
static CompressedAssembly assembly_bundle_System_Xml_dll = {{"System.Xml.dll", assembly_data_System_Xml_dll, 1275904}, 434785};
extern const unsigned char assembly_data_I18N_dll [];
static CompressedAssembly assembly_bundle_I18N_dll = {{"I18N.dll", assembly_data_I18N_dll, 39424}, 12320};
extern const unsigned char assembly_data_Mono_Data_Tds_dll [];
static CompressedAssembly assembly_bundle_Mono_Data_Tds_dll = {{"Mono.Data.Tds.dll", assembly_data_Mono_Data_Tds_dll, 102912}, 39859};
extern const unsigned char assembly_data_Mono_Security_dll [];
static CompressedAssembly assembly_bundle_Mono_Security_dll = {{"Mono.Security.dll", assembly_data_Mono_Security_dll, 299520}, 120825};
extern const unsigned char assembly_data_System_Transactions_dll [];
static CompressedAssembly assembly_bundle_System_Transactions_dll = {{"System.Transactions.dll", assembly_data_System_Transactions_dll, 30720}, 11479};

static const CompressedAssembly *compressed [] = {
	&assembly_bundle_KoloQR_dll,
	&assembly_bundle_MySql_Data_CF_dll,
	&assembly_bundle_Xamarin_Android_Support_v4_dll,
	&assembly_bundle_zxing_monoandroid_dll,
	&assembly_bundle_ZXing_Net_Mobile_dll,
	&assembly_bundle_I18N_West_dll,
	&assembly_bundle_Mono_Android_dll,
	&assembly_bundle_mscorlib_dll,
	&assembly_bundle_System_Core_dll,
	&assembly_bundle_System_Data_dll,
	&assembly_bundle_System_dll,
	&assembly_bundle_System_Windows_dll,
	&assembly_bundle_System_Xml_dll,
	&assembly_bundle_I18N_dll,
	&assembly_bundle_Mono_Data_Tds_dll,
	&assembly_bundle_Mono_Security_dll,
	&assembly_bundle_System_Transactions_dll,
	NULL
};

static char *image_name = "KoloQR.dll";

static void install_dll_config_files (void (register_config_for_assembly_func)(const char *, const char *)) {

}

static const char *config_dir = NULL;
static MonoBundledAssembly **bundled;

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <zlib.h>

static int
my_inflate (const Byte *compr, uLong compr_len, Byte *uncompr, uLong uncompr_len)
{
	int err;
	z_stream stream;

	memset (&stream, 0, sizeof (z_stream));
	stream.next_in = (Byte *) compr;
	stream.avail_in = (uInt) compr_len;

	// http://www.zlib.net/manual.html
	err = inflateInit2 (&stream, 16+MAX_WBITS);
	if (err != Z_OK)
		return 1;

	for (;;) {
		stream.next_out = uncompr;
		stream.avail_out = (uInt) uncompr_len;
		err = inflate (&stream, Z_NO_FLUSH);
		if (err == Z_STREAM_END) break;
		if (err != Z_OK) {
			printf ("%d\n", err);
			return 2;
		}
	}

	err = inflateEnd (&stream);
	if (err != Z_OK)
		return 3;

	if (stream.total_out != uncompr_len)
		return 4;
	
	return 0;
}

void mono_mkbundle_init (void (register_bundled_assemblies_func)(const MonoBundledAssembly **), void (register_config_for_assembly_func)(const char *, const char *))
{
	CompressedAssembly **ptr;
	MonoBundledAssembly **bundled_ptr;
	Bytef *buffer;
	int nbundles;

	install_dll_config_files (register_config_for_assembly_func);

	ptr = (CompressedAssembly **) compressed;
	nbundles = 0;
	while (*ptr++ != NULL)
		nbundles++;

	bundled = (MonoBundledAssembly **) malloc (sizeof (MonoBundledAssembly *) * (nbundles + 1));
	bundled_ptr = bundled;
	ptr = (CompressedAssembly **) compressed;
	while (*ptr != NULL) {
		uLong real_size;
		uLongf zsize;
		int result;
		MonoBundledAssembly *current;

		real_size = (*ptr)->assembly.size;
		zsize = (*ptr)->compressed_size;
		buffer = (Bytef *) malloc (real_size);
		result = my_inflate ((*ptr)->assembly.data, zsize, buffer, real_size);
		if (result != 0) {
			fprintf (stderr, "mkbundle: Error %d decompressing data for %s\n", result, (*ptr)->assembly.name);
			exit (1);
		}
		(*ptr)->assembly.data = buffer;
		current = (MonoBundledAssembly *) malloc (sizeof (MonoBundledAssembly));
		memcpy (current, *ptr, sizeof (MonoBundledAssembly));
		current->name = (*ptr)->assembly.name;
		*bundled_ptr = current;
		bundled_ptr++;
		ptr++;
	}
	*bundled_ptr = NULL;
	register_bundled_assemblies_func((const MonoBundledAssembly **) bundled);
}
